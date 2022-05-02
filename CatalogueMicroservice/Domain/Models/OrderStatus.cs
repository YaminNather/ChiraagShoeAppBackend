public struct OrderStatus
{
    public OrderStatus(string name) => this.name = name;

    public static OrderStatus Parse(String value)
    {
        if(value == Verifying.name)
            return Verifying;
        else if(value == Verified.name)
            return Verified;
        else if(value == CheckedOut.name)
            return CheckedOut;
        else if(value == Delivered.name)
            return Delivered;
        else
            throw new FormatException();
    }

    public override string ToString() => name;


    private string name;


    public static OrderStatus Verifying = new OrderStatus("verifying");
    public static OrderStatus Verified = new OrderStatus("verified");
    public static OrderStatus CheckedOut = new OrderStatus("checked_out");
    public static OrderStatus Delivered = new OrderStatus("delivered");    
}