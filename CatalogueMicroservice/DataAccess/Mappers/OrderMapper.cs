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
            Amount = domainModel.Amount,
            DeliverTo = domainModel.DeliverTo,
            ContactNumber = domainModel.ContactNumber,
            Status = domainModel.Status.ToString()
        };
    }

    public Order ToDomainModel(OrderDataModel dataModel)
    {
        return new Order(
            Product: dataModel.Product!,
            PurchasedBy: dataModel.PurchasedBy!,
            Amount: (float)dataModel.Amount!,
            DeliverTo: dataModel.DeliverTo,
            ContactNumber: dataModel.ContactNumber,
            Status: OrderStatus.Parse((string)dataModel.Status!)
        );
    }
}