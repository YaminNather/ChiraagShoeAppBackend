using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class OrderService
{
    public ConfirmBidResponse ConfirmBid(Bid bidToConfirm, Bid[] productsOtherBids)
    {
        bidToConfirm.UpdateStatus(BidStatus.Accepted);

        foreach(Bid bid in productsOtherBids)
            bid.UpdateStatus(BidStatus.Declined);
        
        Order createdOrder = new Order(bidToConfirm.ProductId, bidToConfirm.Bidder, bidToConfirm.Amount, null, null, OrderStatus.Verifying);
        
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