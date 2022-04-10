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
            throw new BidAmountLessThanPreviousAmountException();

        Amount = value;
    }

    public void UpdateStatus(BidStatus value) => Status = value;

    public string Bidder { get; init; } = null!;

    public string ProductId { get; init; } = null!;
    
    public float Amount { get; private set; }

    public BidStatus Status { get; private set; }

}

class BidAmountLessThanPreviousAmountException : Exception {}