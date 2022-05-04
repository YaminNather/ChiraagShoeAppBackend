using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public interface IProductRepository
{
    Task<Product> Add(AddProductOptions options);

    Task<Product?> Get(String id);

    Task<IEnumerable<Product>> GetAll();

    Task<Product[]> GetAllSellersProducts(string seller);

    Task<Product[]> GetLatestArrivals();

    Task Store(Product product);
}

public class AddProductOptions
{
    public AddProductOptions(
        string Name,
        string Seller,
        string Description,
        float InitialPrice,
        string MainImage,
        List<string>? Images = null
    )
    {
        this.Name = Name;
        this.Seller = Seller;
        this.Description = Description;
        this.InitialPrice = InitialPrice;
        this.MainImage = MainImage;
        this.Images = Images;
    }
    public string Name { get; }

    public string Seller { get; }
    
    public string Description { get; }
    
    public float InitialPrice { get; }

    public string MainImage { get; }

    public List<string>? Images { get; }
}