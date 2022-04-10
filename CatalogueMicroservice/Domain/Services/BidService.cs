using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class BidService
{
    public BidService(IBidRepository bidRepository)
    {
        this.bidRepository = bidRepository;
    }

    public async Task<Bid> PlaceBid(string bidder, string product, float amount, Bid[] existingBids)
    {
        Bid? highestBid = GetHighestBidOfProduct(existingBids);

        if(highestBid != null && amount < highestBid.Amount)
            throw new BidAmountLessThanHighestBidAmountException();
        
        Bid? previousBid = getPreviousBidOnProductByUser(product, bidder, existingBids);

        if(previousBid == null)
            return await bidRepository.AddBid(new AddBidOptions(bidder, product, amount));
        else 
        {
            previousBid.UpdateAmount(amount);
            return await bidRepository.UpdateBid(previousBid);
        }
    }

    public Bid? GetHighestBidOfProduct(Bid[] productBids)
    {
        if(productBids.Length == 0)
            return null;

        Bid r = productBids[0];
        for(int i = 1; i < productBids.Length; i++)
        {
            if(productBids[i].Amount > r.Amount)
                r = productBids[i];
        }

        return r;
    }

    private Bid? getPreviousBidOnProductByUser(string productId, string userId, Bid[] bids)
    {
        for(int i = 0; i < bids.Length; i++)
        {
            if(bids[i].ProductId == productId && bids[i].Bidder == userId)
                return bids[i];
        }

        return null;
    }


    private readonly IBidRepository bidRepository;
}

class BidAmountLessThanHighestBidAmountException : System.Exception {}