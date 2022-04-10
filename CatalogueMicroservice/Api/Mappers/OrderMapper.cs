using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class OrderMapper
{
    public OrderMapper(ProductMapper productMapper, IProductRepository productRepository)
    {
        this.productMapper = productMapper;
        this.productRepository = productRepository;
    }

    public async Task<OrderDto> ToDto(Order domainModel)
    {
        Product product = (await productRepository.Get(domainModel.Product))!;
        ProductDto productDto = productMapper.ToDto(product);

        return new OrderDto(
            Product: productDto,
            PurchasedBy: domainModel.PurchasedBy,
            DeliverTo: domainModel.DeliverTo,
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
}