public struct BidStatus
{
    public BidStatus(string name) => this.name = name;

    public static BidStatus Parse(String value)
    {
        if(value == Pending.name)
            return Pending;
        else if(value == Declined.name)
            return Declined;
        else if(value == Accepted.name)
            return Accepted;
        else
            throw new FormatException($"{value} cannot be parsed to BidStatus");
    }
    
    public override string ToString() => name;



    private string name;

    public static BidStatus Pending = new BidStatus("pending");
    public static BidStatus Declined = new BidStatus("declined");
    public static BidStatus Accepted = new BidStatus("accepted");
}