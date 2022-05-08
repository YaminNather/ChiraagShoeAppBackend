using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

[System.Serializable]
public class OrderDto
{
    public OrderDto(ProductDto Product, UserDto PurchasedBy, string? DeliverTo, string? ContactNumber, float Amount, string Status)
    {
        this.Product = Product;
        this.PurchasedBy = PurchasedBy;
        this.Amount = Amount;
        this.DeliverTo = DeliverTo;
        this.ContactNumber = ContactNumber;
        this.Status = Status;
    }


    public ProductDto Product { get; } = null!;

    public float Amount { get; }
    
    public UserDto PurchasedBy { get; }
    
    public string? DeliverTo { get; }

    public string? ContactNumber { get; }

    public string Status { get; }
}