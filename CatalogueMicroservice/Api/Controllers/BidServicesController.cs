using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BidServicesController : ControllerBase 
{
    public BidServicesController(
        BidService bidService, 
        IBidRepository bidRepository, 
        BidMapper bidMapper, 
        IProductRepository productRepository, 
        ProductMapper productMapper,
        IOrderRepository orderRepository,
        OrderMapper orderMapper
    )
    {
        this.bidService = bidService;
        this.bidRepository = bidRepository;
        this.bidMapper = bidMapper;
        this.productRepository = productRepository;
        this.productMapper = productMapper;
        this.orderRepository = orderRepository;
        this.orderMapper = orderMapper;
    }

    [HttpPost("PlaceBid")]
    public async Task<ActionResult<BidDto>> PlaceBid([FromBody] PlaceBidRequest placeBidRequest)
    {
        Bid[] productBids = await bidRepository.GetBidsOfProduct(placeBidRequest.ProductId);

        Product product = (await productRepository.Get(placeBidRequest.ProductId))!;
        Bid placedBid = await bidService.PlaceBid(placeBidRequest.Bidder, product, placeBidRequest.Amount, productBids);
        
        BidDto bidDto = await bidMapper.ToDto(placedBid);
        return Ok(bidDto);
    }

    // [HttpPost("RemoveBid")]
    // public async Task<ActionResult> RemoveBid([FromBody] RemoveBidRequest removeBidRequest)
    // {
    //     await bidRepository.RemoveBid(removeBidRequest.ProductId);

    //     return new OkResult();
    // }

    [HttpPost("AcceptBid")]
    public async Task<ActionResult<OrderDto>> AcceptBid([FromBody] AcceptBidRequest request)
    {
        List<Bid> bids = new List<Bid>(await bidRepository.GetBidsOfProduct(request.Product));
        Bid? bidToConfirm = bids.Find((bid) => bid.ProductId == request.Product && bid.Bidder == request.Bidder);        
        if(bidToConfirm == null)
            return BadRequest("Bid does not exist");

        bids.Remove(bidToConfirm);
        
        ConfirmBidResponse confirmBidResponse = bidService.AcceptBid(bidToConfirm, bids.ToArray<Bid>());
    
        Order createdOrder = await orderRepository.StoreOrder(confirmBidResponse.Order);
        await bidRepository.UpdateBids(confirmBidResponse.Bids);
                
        OrderDto dto = await orderMapper.ToDto(createdOrder);
        return Ok(dto);
    }

    [HttpGet("GetBidsForProduct")]
    public async Task<ActionResult<BidDto[]>> GetBidsForProduct(string productId)
    {
        Bid[] productBids = await bidRepository.GetBidsOfProduct(productId);
        
        BidDto[] dtos = await bidMapper.ToDtos(productBids);
        return Ok(dtos);
    }

    [HttpGet("GetBidsOfUser")]
    public async Task<ActionResult<BidWithProductDto[]>> GetBidsOfUser(string userId)
    {
        Bid[] bids = await bidRepository.GetBidsOfUser(userId);
        BidWithProductDto[] dtos = await bidMapper.ToWithProductDtos(bids);

        return Ok(dtos);
    }


    private readonly BidService bidService;
    private readonly IBidRepository bidRepository;
    private readonly BidMapper bidMapper;
    private readonly IProductRepository productRepository;
    private readonly ProductMapper productMapper;
    private readonly IOrderRepository orderRepository;
    private readonly OrderMapper orderMapper;
}