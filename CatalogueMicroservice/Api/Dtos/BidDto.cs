using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

public struct BidDto
{
    public BidDto(string Bidder, string ProductId, float Amount, string Status)
    {
        this.Bidder = Bidder;
        this.ProductId = ProductId;
        this.Amount = Amount;
        this.Status = Status;
    }
    
    public string Bidder { get; } = null!;

    public string ProductId { get; } = null!;

    public float Amount { get; }

    public string Status { get; }
}