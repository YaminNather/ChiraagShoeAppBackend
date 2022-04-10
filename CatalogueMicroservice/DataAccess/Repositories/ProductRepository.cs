using Supabase;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;

public class ProductRepository : IProductRepository
{
    public ProductRepository(Client client, ProductMapper mapper)
    {
        this.client = client;
        this.mapper = mapper;
    }

    public async Task<Product> Add(AddProductOptions options)
    {
        ProductDataModel productDataModel = new ProductDataModel 
        {
            Name = options.Name,
            Seller = options.Seller,
            Description = options.Description,
            InitialPrice = options.InitialPrice,
            CreatedAt = options.CreatedAt,
            ModifiedAt = options.ModifiedAt,
            MainImage = options.MainImage,
            Images = options.Images,
            IsAvailable = options.IsAvailable
        };

        ModeledResponse<ProductDataModel> insertResponse = await client.From<ProductDataModel>().Insert(productDataModel);
        return mapper.ToDomainModel(insertResponse.Models[0]);
    }

    public async Task<Product?> Get(String id)
    {
        ModeledResponse<ProductDataModel> results = await client
        .From<ProductDataModel>()
        .Filter("id", Postgrest.Constants.Operator.Equals, int.Parse(id))
        .Get();
        
        if(results.Models.Count() == 0)
            return null;

        return mapper.ToDomainModel(results.Models[0]);
    }

    public async Task<IEnumerable<Product>> GetAll()
    {
        ModeledResponse<ProductDataModel> response = await client.From<ProductDataModel>().Get();
        List<ProductDataModel> dataModels = response.Models;

        return dataModels.Select<ProductDataModel, Product>((element) => mapper.ToDomainModel(element));
    }

    public async Task<Product[]> GetAllSellersProducts(string seller)
    {
        ModeledResponse<ProductDataModel> dataModels = await client.From<ProductDataModel>().Filter("seller", Postgrest.Constants.Operator.Equals, seller).Get();

        Product[] r = dataModels.Models.Select<ProductDataModel, Product>((dataModel) => mapper.ToDomainModel(dataModel)).ToArray();
        return r;
    }

    public async Task<Product[]> GetLatestArrivals()
    {
        ModeledResponse<ProductDataModel> getResponse = await client.From<ProductDataModel>()
        .Order("created_at", Postgrest.Constants.Ordering.Descending)
        .Limit(10)
        .Get();
        
        Product[] r = getResponse.Models.Select<ProductDataModel, Product>((dataModel) => mapper.ToDomainModel(dataModel)).ToArray();

        return r;
    }

    public async Task Store(Product product)
    {
        await client.From<ProductDataModel>().Insert(mapper.FromDomainModel(product));
    }


    private readonly Client client;
    private readonly ProductMapper mapper;
}