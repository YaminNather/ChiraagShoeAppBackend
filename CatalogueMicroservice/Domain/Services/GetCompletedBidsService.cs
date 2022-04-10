using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public class GetCompletedBidsService
{
    public GetCompletedBidsService(IBidRepository bidRepository, IProductRepository productRepository)
    {
        this.bidRepository = bidRepository;
        this.productRepository = productRepository;
    }    


    private readonly IBidRepository bidRepository;
    private readonly IProductRepository productRepository;
}