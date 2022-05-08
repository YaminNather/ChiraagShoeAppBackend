using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class ProductMapper
{
    public ProductMapper(IUserRepository userRepository, UserMapper userMapper)
    {
        this.userRepository = userRepository;
        this.userMapper = userMapper;
    }

    public async Task<ProductDto> ToDto(Product domainModel)
    {
        User seller = (await userRepository.Get(domainModel.Seller))!;

        return new ProductDto 
        { 
            Id = domainModel.Id,
            Name = domainModel.Name,
            Seller = userMapper.ToDto(seller),
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

    public async Task<ProductDto[]> ToDtos(Product[] domainModels)
    {
        ProductDto[] r = new ProductDto[domainModels.Length];
        for(int i = 0; i < domainModels.Length; i++)
            r[i] = await ToDto(domainModels[i]);

        return r;
    }

    public Product ToDomainModel(ProductDto dto, DateTime createdAt, DateTime modifiedAt)
    {        
        return new Product(
            Id: dto.Id,
            Name: dto.Name,
            Seller: dto.Seller.Id, 
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

    private readonly IUserRepository userRepository;
    private readonly  UserMapper userMapper;
}