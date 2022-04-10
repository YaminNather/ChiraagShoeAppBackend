namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class Order
{
    public Order(string Product, string PurchasedBy, string DeliverTo, OrderStatus Status)
    {
        this.Product = Product;
        this.PurchasedBy = PurchasedBy;
        this.DeliverTo = DeliverTo;
        this.Status = Status;
    }


    public string Product { get; } = null!;
    
    public string PurchasedBy { get; } = null!;
    
    public string DeliverTo { get; } = null!;

    public OrderStatus Status { get; }
}