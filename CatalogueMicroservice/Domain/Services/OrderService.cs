using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class OrderService
{
    public ConfirmBidResponse ConfirmBid(Bid bidToConfirm, string deliverTo, Bid[] productsOtherBids)
    {
        bidToConfirm.UpdateStatus(BidStatus.accepted);

        foreach(Bid bid in productsOtherBids)
            bid.UpdateStatus(BidStatus.declined);
        
        Order createdOrder = new Order(bidToConfirm.ProductId, bidToConfirm.Bidder, deliverTo, OrderStatus.verifying);
        
        List<Bid> updatedBids = new List<Bid>(productsOtherBids);
        updatedBids.Add(bidToConfirm);
        
        return new ConfirmBidResponse(createdOrder, updatedBids.ToArray());
    }
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