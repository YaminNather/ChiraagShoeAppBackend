using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class BidMapper
{
    public BidMapper(IProductRepository productRepository, ProductMapper productMapper)
    {
        this.productRepository = productRepository;
        this.productMapper = productMapper;
    }

    public BidDto ToDto(Bid domainModel)
    {
        return new BidDto(
            Bidder: domainModel.Bidder,
            ProductId: domainModel.ProductId,
            Amount: domainModel.Amount,
            Status: domainModel.Status.ToString()
        );
    }

    public async Task<BidWithProductDto> ToWithProductDto(Bid bid)
    {
        Product product = (await productRepository.Get(bid.ProductId))!;

        return new BidWithProductDto(
            Bidder: bid.Bidder,
            Product: productMapper.ToDto(product),
            Amount: bid.Amount,
            Status: bid.Status.ToString()
        );
    }

    public BidWithProductDto ToWithProductDto(Bid bid, ProductDto product)
    {
        return new BidWithProductDto(
            Bidder: bid.Bidder,
            Product: product,
            Amount: bid.Amount,
            Status: bid.Status.ToString()
        );
    }


    private readonly IProductRepository productRepository;
    private ProductMapper productMapper;
}