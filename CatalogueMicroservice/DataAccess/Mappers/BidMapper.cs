using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

public class BidMapper
{
    public BidDataModel ToDataModel(Bid domainModel)
    {
        return new BidDataModel
        {            
            Bidder = domainModel.Bidder, 
            ProductId = domainModel.ProductId, 
            Amount = domainModel.Amount,
            Status = domainModel.Status.ToString()
        };
    }

    public Bid ToDomainModel(BidDataModel dataModel)
    {
        return new Bid(Bidder: dataModel.Bidder, ProductId: dataModel.ProductId, Amount: dataModel.Amount, Status: BidStatus.Parse(dataModel.Status));
    }
}