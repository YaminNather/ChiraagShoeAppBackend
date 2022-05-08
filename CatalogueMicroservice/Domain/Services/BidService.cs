using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class BidService
{
    public BidService(IBidRepository bidRepository, IOrderRepository orderRepository)
    {
        this.bidRepository = bidRepository;
        this.orderRepository = orderRepository;
    }

    public async Task<Bid> PlaceBid(string bidder, Product product, float amount, Bid[] existingBids)
    {
        if(amount <= product.InitialPrice)
            throw new InvalidOperationException("Bid amount less than initial Price");
        
        Bid? previousBid = getPreviousBidOnProductByUserFromBids(bidder, existingBids);

        if(previousBid == null)
            return await bidRepository.AddBid(new AddBidOptions(bidder, product.Id, amount));
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

    public ConfirmBidResponse AcceptBid(Bid bidToConfirm, Bid[] productsOtherBids)
    {
        bidToConfirm.Accept();

        foreach(Bid bid in productsOtherBids)
            bid.Decline();
        
        Order createdOrder = new Order(bidToConfirm.ProductId, bidToConfirm.Bidder, bidToConfirm.Amount, null, null, OrderStatus.Verifying);
        
        List<Bid> updatedBids = new List<Bid>(productsOtherBids);
        updatedBids.Add(bidToConfirm);
        
        return new ConfirmBidResponse(createdOrder, updatedBids.ToArray());
    }

    private Bid? getPreviousBidOnProductByUserFromBids(string userId, Bid[] bids)
    {
        for(int i = 0; i < bids.Length; i++)
        {
            if(bids[i].Bidder == userId)
                return bids[i];
        }

        return null;
    }


    private readonly IBidRepository bidRepository;
    private readonly IOrderRepository orderRepository;
}

public class ConfirmBidResponse
{
    public ConfirmBidResponse(Order Order, Bid[] Bids)
    {
        this.Order = Order;
        this.Bids = Bids;
    }


    public Order Order { get; }
    public Bid[] Bids { get; }
}