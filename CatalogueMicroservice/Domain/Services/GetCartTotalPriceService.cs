using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class GetCartTotalPriceService
{
    public GetCartTotalPriceService(ProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<float> GetTotalPrice(Cart cart)
    {
        float r = 0.0f;
        for(int i = 0; i < cart.items.Length; i++)
        {
            Product? product = await productRepository.Get(cart.items[i].Product);
            if(product == null)
                throw new Exception();

            r += product.InitialPrice * cart.items[i].Quantity;
        }

        return r;
    }



    private readonly ProductRepository productRepository; 
}