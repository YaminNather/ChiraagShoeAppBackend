namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class SoldItemDto
{
    public SoldItemDto(ProductDto Product, BidDto? Bid = null, OrderDto? Order = null)
    {
        this.Product = Product;
        this.Bid = Bid;
        this.Order = Order;
    }

    public ProductDto Product { get; init; }

    public BidDto? Bid { get; init; }
    
    public OrderDto? Order { get; init; }
}