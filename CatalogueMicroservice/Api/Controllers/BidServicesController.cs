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
        ProductMapper productMapper
    )
    {
        this.bidService = bidService;
        this.bidRepository = bidRepository;
        this.bidMapper = bidMapper;
        this.productRepository = productRepository;
        this.productMapper = productMapper;
    }

    [HttpPost("PlaceBid")]
    public async Task<ActionResult<BidDto>> PlaceBid([FromBody] PlaceBidRequest placeBidRequest)
    {
        Bid[] productBids = await bidRepository.GetBidsOfProduct(placeBidRequest.ProductId);

        Bid placedBid = await bidService.PlaceBid(placeBidRequest.Bidder, placeBidRequest.ProductId, placeBidRequest.Amount, productBids);
        
        return Ok(bidMapper.ToDto(placedBid));
    }


    [HttpPost("RemoveBid")]
    public async Task<ActionResult> RemoveBid([FromBody] RemoveBidRequest removeBidRequest)
    {
        await bidRepository.RemoveBid(removeBidRequest.ProductId);

        return new OkResult();
    }

    [HttpGet("GetBidsForProduct")]
    public async Task<ActionResult<BidDto[]>> GetBidsForProduct(string productId)
    {
        Bid[] productBids = await bidRepository.GetBidsOfProduct(productId);
        
        BidDto[] dtos = productBids.Select<Bid, BidDto>((bid) => bidMapper.ToDto(bid)).ToArray<BidDto>();
        return Ok(dtos);
    }

    [HttpGet("GetBidsOfUser")]
    public async Task<ActionResult<BidWithProductDto>> GetBidsOfUser(string userId)
    {
        Bid[] bids = await bidRepository.GetBidsOfUser(userId);
        List<BidWithProductDto> r = new List<BidWithProductDto>();
        foreach(Bid bid in bids)
        {
            Product? product = await productRepository.Get(bid.ProductId);
            if(product == null)
                throw new Exception();
            
            BidWithProductDto dto = new BidWithProductDto(bid.Bidder, productMapper.ToDto(product), bid.Amount, bid.Status.ToString());
            r.Add(dto);
        }

        return Ok(r);
    }


    private readonly BidService bidService;
    private readonly IBidRepository bidRepository;
    private readonly BidMapper bidMapper;
    private readonly IProductRepository productRepository;
    private readonly ProductMapper productMapper;    
}