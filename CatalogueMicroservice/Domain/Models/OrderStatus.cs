public struct OrderStatus
{
    public OrderStatus(string name) => this.name = name;

    public static OrderStatus Parse(String value)
    {
        if(value == verifying.name)
            return verifying;
        else if(value == delivering.name)
            return delivering;
        else if(value == delivered.name)
            return delivered;
        else
            throw new FormatException();
    }

    public override string ToString() => name;


    private string name;

    public static OrderStatus verifying = new OrderStatus("verifying");
    public static OrderStatus delivering = new OrderStatus("delivered");
    public static OrderStatus delivered = new OrderStatus("delivered");
}