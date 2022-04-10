using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public interface IBidRepository
{
    Task<Bid?> GetBid(string productId, string userId);
    Task<Bid[]> GetBidsOfUser(string userId);
    Task<Bid[]> GetBidsOfProduct(string productId);
    Task<Bid?> GetHighestBidOfProduct(string product);
    Task<Bid> AddBid(AddBidOptions options);
    Task<Bid> UpdateBid(Bid bid);
    Task UpdateBids(Bid[] bids);
    Task RemoveBid(string bidId);
}

public struct AddBidOptions
{
    public AddBidOptions(string Bidder, string ProductId, float Amount)
    {
        this.Bidder = Bidder;
        this.ProductId = ProductId;
        this.Amount = Amount;
    }

    public string Bidder { get; init; } = null!;

    public string ProductId { get; init; } = null!;
    
    public float Amount { get; init; }
}