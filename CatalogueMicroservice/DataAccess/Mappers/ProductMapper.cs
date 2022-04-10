using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

public class ProductMapper
{
    public Product ToDomainModel(ProductDataModel dataModel)
    {
        int id = (int)dataModel.Id!;
        Product r = new Product(
            Id: id.ToString(), 
            Name: dataModel.Name,
            Seller: dataModel.Seller,
            Description: dataModel.Description, 
            Category: null,
            // Category: (dataModel.Category != null) ? Category.FromID(dataModel.Category) : null,
            InitialPrice: dataModel.InitialPrice,
            CreatedAt: dataModel.CreatedAt,
            ModifiedAt: dataModel.ModifiedAt,
            MainImage: dataModel.MainImage,
            Images: (dataModel.Images != null) ? dataModel.Images : null,
            IsAvailable: dataModel.IsAvailable
        );

        return r;
    }

    public ProductDataModel FromDomainModel(Product product)
    {
        ProductDataModel r = new ProductDataModel
        {
            Id = int.Parse(product.Id),
            Name = product.Name,
            Seller = product.Seller,
            Description = product.Description, 
            // Category = (product.Category != null) ? product.Category.name : null,
            InitialPrice = product.InitialPrice,
            CreatedAt = product.CreatedAt,
            ModifiedAt = product.ModifiedAt,
            MainImage = product.MainImage,
            Images = (product.Images != null) ? new List<string>(product.Images) : null,
            IsAvailable = product.IsAvailable
        };
        
        return r;
    }

}