using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

public struct BidWithProductDto
{
    public BidWithProductDto(string Bidder, ProductDto Product, float Amount, string Status)
    {
        this.Bidder = Bidder;
        this.Product = Product;
        this.Amount = Amount;
        this.Status = Status;
    }
    
    public string Bidder { get; } = null!;

    public ProductDto Product { get; }

    public float Amount { get; }

    public string Status { get; }
}