using Supabase;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;

public class SupabaseBidRepository : IBidRepository
{
    public SupabaseBidRepository(Client client, BidMapper bidMapper)
    {
        this.client = client;
        this.bidMapper = bidMapper;
    }

    public async Task<Bid?> GetBid(string productId, string userId)
    {
        ModeledResponse<BidDataModel> insertResponse = await client.From<BidDataModel>()
        .Filter("product_id", Postgrest.Constants.Operator.Equals, productId)
        .Filter("bidder", Postgrest.Constants.Operator.Equals, userId)
        .Get();

        if(insertResponse.Models.Count == 0)
            return null;

        return bidMapper.ToDomainModel(insertResponse.Models[0]);
    }    
    
    public async Task<Bid?> GetHighestBidOfProduct(string product)
    {
        ModeledResponse<BidDataModel> insertResponse = await client.From<BidDataModel>()
        .Filter("product_id", Postgrest.Constants.Operator.Equals, product)
        .Order("amount", Postgrest.Constants.Ordering.Descending)
        .Limit(1)
        .Get();

        if(insertResponse.Models.Count == 0)
            return null;

        return bidMapper.ToDomainModel(insertResponse.Models[0]);
    }    

    public async Task<Bid> AddBid(AddBidOptions options)
    {
        BidDataModel dataModel = new BidDataModel 
        { 
            Bidder = options.Bidder, 
            ProductId = options.ProductId, 
            Amount = options.Amount,
            Status = BidStatus.Pending.ToString()
        };
        
        ModeledResponse<BidDataModel> insertResponse = await client.From<BidDataModel>().Insert(dataModel);

        return bidMapper.ToDomainModel(insertResponse.Models[0]);
    }

    public async Task<Bid> UpdateBid(Bid bid)
    {
        BidDataModel dataModel = bidMapper.ToDataModel(bid);
        
        await RemoveBid(bid.Bidder, bid.ProductId);
        ModeledResponse<BidDataModel> insertResponse = await client.From<BidDataModel>().Insert(dataModel);

        return bidMapper.ToDomainModel(insertResponse.Models[0]);
    }

    public async Task UpdateBids(Bid[] bids)
    {
        foreach(Bid bid in bids)
            await UpdateBid(bid);
    }

    public async Task RemoveBid(string bidder, string product)
    {
        await client.From<BidDataModel>()
        .Filter("bidder", Postgrest.Constants.Operator.Equals, bidder)
        .Filter("product_id", Postgrest.Constants.Operator.Equals, product)
        .Delete();
    }

    public async Task<Bid[]> GetBidsOfUser(string userId)
    {
        ModeledResponse<BidDataModel> queryResponse = await client.From<BidDataModel>()
        .Filter("bidder", Postgrest.Constants.Operator.Equals, userId)
        .Get();
        return queryResponse.Models.Select<BidDataModel, Bid>((dataModel) => bidMapper.ToDomainModel(dataModel)).ToArray<Bid>();
    }

    public async Task<Bid[]> GetBidsOfProduct(string productId)
    {
        ModeledResponse<BidDataModel> queryResponse = await client.From<BidDataModel>()
        .Filter("product_id", Postgrest.Constants.Operator.Equals, productId)
        .Order("amount", Postgrest.Constants.Ordering.Ascending)
        .Get();
        return queryResponse.Models.Select<BidDataModel, Bid>((dataModel) => bidMapper.ToDomainModel(dataModel)).ToArray<Bid>();
    }


    private readonly Client client;

    private readonly BidMapper bidMapper;
}