namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

public class Cart
{
    public Cart(CartItem[] items)
    {
        this.items = items;
    }

    public void AddToCart(Product product, int quantity)
    {
        CartItem? cartItem = findCartItemOfProduct(product);

        if(cartItem != null)
            cartItem.Add(quantity);
    }

    private CartItem? findCartItemOfProduct(Product product)
    {
        for(int i = 0; i < items.Length; i++)
        {
            if(items[i].Product == product.Id)
                return items[i];
        }

        return null;
    }


    public CartItem[] items;
}