namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class Bid
{
    public Bid(string Bidder, string ProductId, float Amount, BidStatus Status)
    {        
        this.Bidder = Bidder;
        this.ProductId = ProductId;
        this.Amount = Amount;
        this.Status = Status;
    }

    public void UpdateAmount(float value)
    {
        if(value <= Amount)
            throw new InvalidOperationException("Bid amount less than previous amount");

        Amount = value;
    }

    // public void UpdateStatus(BidStatus value) => Status = value;

    public void Accept()
    {
        if(!Status.Equals(BidStatus.Pending))
            throw new InvalidOperationException("Cant accept a bid that's not pending");

        Status = BidStatus.Accepted;
    }

    public void Decline()
    {
        if(!Status.Equals(BidStatus.Pending))
            throw new InvalidOperationException("Cant decline a bid that's not pending");

        Status = BidStatus.Declined;
    }

    public string Bidder { get; init; } = null!;

    public string ProductId { get; init; } = null!;
    
    public float Amount { get; private set; }

    public BidStatus Status { get; private set; }

}