using ChiraagShoeAppBackend.CatalogueMicroservice.Api.Dtos;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Api.Mappers;

public class BidMapper
{
    public BidMapper(
        IProductRepository productRepository, 
        ProductMapper productMapper, 
        IUserRepository userRepository,
        UserMapper userMapper
    )
    {
        this.productRepository = productRepository;
        this.productMapper = productMapper;
        this.userRepository = userRepository;
        this.userMapper = userMapper;
    }

    public async Task<BidDto> ToDto(Bid domainModel)
    {
        User? bidder  = await userRepository.Get(domainModel.Bidder);
        if(bidder == null)
            throw new Exception("User cannot be null");

        return new BidDto(            
            Bidder: userMapper.ToDto(bidder),
            ProductId: domainModel.ProductId,
            Amount: domainModel.Amount,
            Status: domainModel.Status.ToString()
        );
    }

    public async Task<BidDto[]> ToDtos(Bid[] domainModels)
    {
        BidDto[] r = new BidDto[domainModels.Length];
        for(int i = 0; i < domainModels.Length; i++)
            r[i] = await ToDto(domainModels[i]);

        return r;
    }

    public async Task<BidWithProductDto> ToWithProductDto(Bid domainModel)
    {
        User? bidder = await userRepository.Get(domainModel.Bidder);
        if(bidder == null)
            throw new Exception("User cannot be null");

        Product product = (await productRepository.Get(domainModel.ProductId))!;

        return new BidWithProductDto(
            Bidder: userMapper.ToDto(bidder),
            Product: await productMapper.ToDto(product),
            Amount: domainModel.Amount,
            Status: domainModel.Status.ToString()
        );
    }

    public async Task<BidWithProductDto> ToWithProductDto(Bid bid, ProductDto product)
    {
        User? bidder = await userRepository.Get(bid.Bidder);
        if(bidder == null)
            throw new Exception("User cannot be null");

        return new BidWithProductDto(
            Bidder: userMapper.ToDto(bidder),
            Product: product,
            Amount: bid.Amount,
            Status: bid.Status.ToString()
        );
    }

    public async Task<BidWithProductDto[]> ToWithProductDtos(Bid[] bids)
    {
        BidWithProductDto[] r = new BidWithProductDto[bids.Length];
        for(int i = 0; i < bids.Length; i++)
            r[i] = await ToWithProductDto(bids[i]);

        return r;
    }


    private readonly IProductRepository productRepository;
    private readonly ProductMapper productMapper;
    private readonly IUserRepository userRepository;
    private readonly UserMapper userMapper;
}