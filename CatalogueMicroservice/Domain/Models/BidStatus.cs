public struct BidStatus
{
    public BidStatus(string name) => this.name = name;

    public static BidStatus Parse(String value)
    {
        if(value == pending.name)
            return pending;
        else if(value == declined.name)
            return declined;
        else if(value == accepted.name)
            return accepted;
        else
            throw new FormatException($"{value} cannot be parsed to BidStatus");
    }
    
    public override string ToString() => name;



    private string name;

    public static BidStatus pending = new BidStatus("pending");
    public static BidStatus declined = new BidStatus("declined");
    public static BidStatus accepted = new BidStatus("accepted");
}