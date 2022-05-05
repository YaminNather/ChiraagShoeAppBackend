using Microsoft.AspNetCore.Mvc;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InventoryController : ControllerBase
{
    public InventoryController(
        IProductRepository productRepository, 
        ProductMapper productMapper,
        ProductService productService,
        Supabase.Client client,
        IBidRepository bidRepository,
        BidMapper bidMapper,
        IOrderRepository orderRepository,
        OrderMapper orderMapper
    )
    {
        this.productRepository = productRepository;
        this.productMapper = productMapper;
        this.productService = productService;
        this.client = client;
        this.bidRepository = bidRepository;
        this.bidMapper = bidMapper;
        this.orderRepository = orderRepository;
        this.orderMapper = orderMapper;
    }

    [HttpGet("GetProduct")]
    public async Task<ProductDto?> GetProduct(String id)
    {
        Product? product = await productRepository.Get(id);
        
        if(product == null)
            return null;

        ProductDto r = productMapper.ToDto(product);
        return r;
    }

    [HttpGet("GetAllProducts")]
    public async Task<IEnumerable<ProductDto>> GetAllProducts()
    {
        IEnumerable<Product> products = await productRepository.GetAll();

        return products.Select<Product, ProductDto>((element) => productMapper.ToDto(element));
    }    

    [HttpPost("AddProduct")]
    public async Task<ProductDto?> AddProduct([FromBody]AddProductRequest request)
    {
        AddProductOptions addProductOptions = new AddProductOptions(
            Name: request.Name,
            Seller: request.Seller,
            Description: request.Description,
            InitialPrice: request.InitialPrice,
            MainImage: request.MainImage,
            Images: request.Images
        );
        Product product = await productRepository.Add(addProductOptions);

        return productMapper.ToDto(product);
    }

    [HttpPost("SellBid")]
    public async Task<ActionResult<ProductDto>> SellBid([FromBody]DuplicateProductRequest request)
    {
        Product? productToClone = await productRepository.Get(request.Product);
        if (productToClone == null)
            throw new Exception($"Product with id {request.Product} does not exist");

        Product product = await productService.DuplicateProduct(productToClone, request.Seller, request.InitialPrice);

        ProductDto dto = productMapper.ToDto(product);
        return Ok(dto);
    }

    [HttpGet("GetAllSellersProducts")]
    public async Task<ActionResult<Dictionary<string, object?>>> GetAllSellersProducts(string seller) 
    {
        List<SoldItemDto> soldItemDtos = new List<SoldItemDto>();
        
        Product[] products = await productRepository.GetAllSellersProducts(seller);
        foreach(Product product in products)
        {
            SoldItemDto? soldItemDto = null;
            if(product.IsAvailable)
            {
                Bid? highestBid = await bidRepository.GetHighestBidOfProduct(product.Id);
                if(highestBid != null)
                {
                    BidDto bidDto = await bidMapper.ToDto(highestBid);
                    soldItemDto = new SoldItemDto(productMapper.ToDto(product), Bid: bidDto);
                }
                else
                    soldItemDto = new SoldItemDto(productMapper.ToDto(product));
            }
            else
            {
                Order order = (await orderRepository.GetOrder(product.Id))!;
                soldItemDto = new SoldItemDto(productMapper.ToDto(product), Order: await orderMapper.ToDto(order));
            }

            if(soldItemDto != null)
                soldItemDtos.Add(soldItemDto);
        }

        return Ok(soldItemDtos);
    }

    [HttpGet("GetLatestArrivals")]
    public async Task<ActionResult<ProductDto[]>> GetLatestArrivals()
    {
        Product[] products = await productRepository.GetLatestArrivals();

        ProductDto[] dtos = products.Select<Product, ProductDto>((product) => productMapper.ToDto(product)).ToArray();
        return Ok(dtos);
    }


    private readonly IProductRepository productRepository;
    private readonly ProductService productService;
    private readonly ProductMapper productMapper;
    private readonly IBidRepository bidRepository;
    private readonly BidMapper bidMapper;
    private readonly IOrderRepository orderRepository;
    private readonly OrderMapper orderMapper;
    private readonly Supabase.Client client;
}