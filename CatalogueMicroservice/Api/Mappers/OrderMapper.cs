using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class OrderMapper
{
    public OrderMapper(ProductMapper productMapper, IProductRepository productRepository, IUserRepository userRepository, UserMapper userMapper)
    {
        this.productMapper = productMapper;
        this.productRepository = productRepository;
        this.userRepository = userRepository;
        this.userMapper = userMapper;
    }

    public async Task<OrderDto> ToDto(Order domainModel)
    {
        Product product = (await productRepository.Get(domainModel.Product))!;
        ProductDto productDto = await productMapper.ToDto(product);

        User purchasedBy = (await userRepository.Get(domainModel.PurchasedBy))!;

        return new OrderDto(
            Product: productDto,
            PurchasedBy: userMapper.ToDto(purchasedBy),
            Amount: domainModel.Amount,
            DeliverTo: domainModel.DeliverTo,
            ContactNumber: domainModel.ContactNumber,
            Status: domainModel.Status.ToString()
        );
    }

    public async Task<OrderDto[]> ToDtos(Order[] orders)
    {
        OrderDto[] r = new OrderDto[orders.Length];
        for(int i = 0; i < orders.Length; i++)
        {
            r[i] = await ToDto(orders[i]);
        }

        return r;
    }


    private readonly IProductRepository productRepository;    
    private readonly ProductMapper productMapper;
    private readonly IUserRepository userRepository;
    private readonly UserMapper userMapper;
}