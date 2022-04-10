namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class CartItem
{
    public CartItem(string Product, int Quantity)
    {
        this.Product = Product;
        this.Quantity = Quantity;
    }

    public void Add(int quantity)
    {
        if(quantity < 1)
            throw new Exception();

        Quantity += quantity;
    }

    public string Product { get; }
    public int Quantity { get; private set; }
}