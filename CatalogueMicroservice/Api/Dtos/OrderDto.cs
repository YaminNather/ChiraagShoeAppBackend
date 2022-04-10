namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

public class OrderDto
{
    public OrderDto(ProductDto Product, string PurchasedBy, string DeliverTo, string Status)
    {
        this.Product = Product;
        this.PurchasedBy = PurchasedBy;
        this.DeliverTo = DeliverTo;
        this.Status = Status;
    }


    public ProductDto Product { get; } = null!;
    
    public string PurchasedBy { get; } = null!;
    
    public string DeliverTo { get; } = null!;

    public string Status { get; }
}