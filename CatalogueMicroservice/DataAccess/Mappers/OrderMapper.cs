using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

public class OrderMapper
{
    public OrderDataModel ToDataModel(Order domainModel)
    {
        return new OrderDataModel
        {
            Product = domainModel.Product,
            PurchasedBy = domainModel.PurchasedBy,
            DeliverTo = domainModel.DeliverTo,
            Status = domainModel.Status.ToString()
        };
    }

    public Order ToDomainModel(OrderDataModel dataModel)
    {
        return new Order(
            Product: dataModel.Product, 
            PurchasedBy: dataModel.PurchasedBy, 
            DeliverTo: dataModel.DeliverTo,
            Status: OrderStatus.Parse(dataModel.Status)
        );
    }
}