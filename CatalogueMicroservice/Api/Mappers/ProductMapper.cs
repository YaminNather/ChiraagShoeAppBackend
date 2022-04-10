using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class ProductMapper
{
    public ProductDto ToDto(Product domainModel)
    {
        return new ProductDto 
        { 
            Id = domainModel.Id,
            Name = domainModel.Name,
            Seller = domainModel.Seller,
            Description = domainModel.Description,
            InitialPrice = domainModel.InitialPrice,
            Category = domainModel.Category?.name,
            MainImage = domainModel.MainImage,
            Images = (domainModel.Images != null) ? new List<string>(domainModel.Images) : null,
            CreatedAt = domainModel.CreatedAt,
            ModifiedAt = domainModel.ModifiedAt,
            IsAvailable = domainModel.IsAvailable
        };
    }

    public Product ToDomainModel(ProductDto dto, DateTime createdAt, DateTime modifiedAt)
    {
        return new Product(
            Id: dto.Id,
            Name: dto.Name,
            Seller: dto.Seller, 
            Description: dto.Description, 
            Category: Category.FromID(dto.Category),
            InitialPrice: dto.InitialPrice, 
            CreatedAt: createdAt,
            ModifiedAt: modifiedAt,
            MainImage: dto.MainImage,
            Images: (dto.Images != null) ? dto.Images : null,
            IsAvailable: dto.IsAvailable
        );
    }
}