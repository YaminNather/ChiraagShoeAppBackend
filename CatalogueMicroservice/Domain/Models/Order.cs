namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class Order
{
    public Order(string Product, string PurchasedBy, float Amount, string? DeliverTo, string? ContactNumber, OrderStatus Status)
    {
        this.Product = Product;
        this.PurchasedBy = PurchasedBy;
        this.Amount = Amount;
        this.DeliverTo = DeliverTo;
        this.ContactNumber = ContactNumber;
        this.Status = Status;
    }

    public void MarkAsPaid()
    {
        if(Status.Equals(OrderStatus.Verified))
            throw new Exception("Order can only be marked as paid only after being verified");

        Status = OrderStatus.CheckedOut;
    }

    public void CompleteCheckout(String address, String contactNumber)
    {
        if(!Status.Equals(OrderStatus.Verified))
            throw new Exception($"Cannot complete checkout when order status is {Status.ToString()}");

        DeliverTo = address;
        ContactNumber = contactNumber;
        Status = OrderStatus.CheckedOut;
    }


    public string Product { get; } = null!;
    
    public string PurchasedBy { get; } = null!;

    public float Amount { get; }
    
    public string? DeliverTo { get; set; }

    public string? ContactNumber { get; set; }

    public OrderStatus Status { get; private set; }
}