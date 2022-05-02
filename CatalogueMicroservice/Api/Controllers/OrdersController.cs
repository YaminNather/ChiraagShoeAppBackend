using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    public OrdersController(
        OrderService orderService, 
        IBidRepository bidRepository, 
        IOrderRepository orderRepository, 
        OrderMapper orderMapper        
    )
    {
        this.orderService = orderService;
        this.bidRepository = bidRepository;
        this.orderRepository = orderRepository;
        this.orderMapper = orderMapper;
    }

    [HttpPost("ConfirmBid")]
    public async Task<ActionResult<OrderDto>> ConfirmBid([FromBody] ConfirmBidRequest request)
    {
        List<Bid> bids = new List<Bid>(await bidRepository.GetBidsOfProduct(request.Product));
        Bid? bidToConfirm = bids.Find((bid) => bid.ProductId == request.Product && bid.Bidder == request.Bidder);        
        if(bidToConfirm == null)
            return BadRequest("Bid does not exist");

        bids.Remove(bidToConfirm);
        
        ConfirmBidResponse confirmBidResponse = orderService.ConfirmBid(bidToConfirm, bids.ToArray<Bid>());
        
        Order createdOrder = await orderRepository.StoreOrder(confirmBidResponse.Order);
        await bidRepository.UpdateBids(confirmBidResponse.Bids);
                
        OrderDto dto = await orderMapper.ToDto(createdOrder);
        return Ok(dto);
    }

    [HttpPost("ConfirmPayment")]
    public async Task<ActionResult<OrderDto>> ConfirmPayment(ConfirmPaymentRequest request) 
    {
        Order? order = await orderRepository.GetOrder(request.Product);
        if(order == null)
            return NotFound($"Order for product {request.Product} not found");

        order.MarkAsPaid();
        
        await orderRepository.StoreOrder(order);

        OrderDto r = await orderMapper.ToDto(order);
        return Ok(r);
    }

    [HttpGet("GetOrdersPurchasedBy")]
    public async Task<ActionResult<OrderDto[]>> GetOrdersPurchasedBy(string purchasedBy)
    {
        Order[] orders = await orderRepository.GetOrdersPurchasedBy(purchasedBy);

        OrderDto[] orderDtos = await orderMapper.ToDtos(orders);
        return Ok(orderDtos);
    }

    [HttpGet("GetOrderForProduct")]
    public async Task<ActionResult<Order>> GetOrderForProduct(string product)
    {
        Order? order = await orderRepository.GetOrder(product);
        if(order == null)
            return NotFound($"Product {product} not found");


        OrderDto r = await orderMapper.ToDto(order);
        return Ok(r);
    }

    [HttpPost("CompleteCheckout")]
    public async Task<ActionResult<Order>> CompleteCheckout(CompleteCheckoutRequest request)
    {
        Order? order = await orderRepository.GetOrder(request.Product);
        if(order == null)
            return NotFound($"Order for product {request.Product} not found");

        order.CompleteCheckout(request.Address, request.ContactNumber);

        order = await orderRepository.StoreOrder(order);

        OrderDto orderDto = await orderMapper.ToDto(order);
        return Ok(orderDto);
    }


    private readonly IBidRepository bidRepository;
    private readonly OrderService orderService;    
    private readonly IOrderRepository orderRepository;
    private readonly OrderMapper orderMapper;
}