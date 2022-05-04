using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class ProductService
{
    public ProductService(IProductRepository productRepository)
    {
        this.productRepository = productRepository;
    }

    public async Task<Product> DuplicateProduct(Product productToClone, String seller, float initialPrice)
    {
        AddProductOptions addProductOptions = new AddProductOptions(
            Name: productToClone.Name,
            Seller: seller,
            Description: productToClone.Description,
            InitialPrice: initialPrice,
            MainImage: productToClone.MainImage,
            Images: productToClone.Images
        );
        Product r = await productRepository.Add(addProductOptions);
        return r;
    }



    private IProductRepository productRepository;
}