using Supabase;
using Postgrest.Responses;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;
using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.DataModels;
using ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Mappers;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.DataAccess.Repositories;

public class SupabaseOrderRepository : IOrderRepository
{
    public SupabaseOrderRepository(OrderMapper orderMapper, Client client)
    {
        this.orderMapper = orderMapper;
        this.client = client;
    }

    public async Task<Order> CreateOrder(string product, string purchasedBy, float amount)
    {
        OrderDataModel orderDataModel = new OrderDataModel
        {
            Product = product,
            PurchasedBy = purchasedBy,
            Amount = amount
        };

        ModeledResponse<OrderDataModel> response = await client.From<OrderDataModel>().Insert(orderDataModel);                    

        Order r = orderMapper.ToDomainModel(response.Models[0]);
        return r;
    }

    public async Task<Order?> GetOrder(string product)
    {
        ModeledResponse<OrderDataModel> response = await client.From<OrderDataModel>()
        .Filter("product", Postgrest.Constants.Operator.Equals, int.Parse(product))
        .Get();

        if(response.Models.Count == 0)
            return null;
        
        return orderMapper.ToDomainModel(response.Models.First());
    }    
    
    public async Task<Order[]> GetOrdersPurchasedBy(string purchasedBy)
    {
        ModeledResponse<OrderDataModel> response = await client.From<OrderDataModel>()
        .Filter("purchased_by", Postgrest.Constants.Operator.Equals, purchasedBy)
        .Get();
        
        Order[] domainModels = response.Models.Select<OrderDataModel, Order>((dataModel) => orderMapper.ToDomainModel(dataModel)).ToArray();
        return domainModels;
    }    
    public async Task<Order> StoreOrder(Order order)
    {
        OrderDataModel dataModel = orderMapper.ToDataModel(order);
        ModeledResponse<OrderDataModel> response = await client.From<OrderDataModel>().Upsert(dataModel);

        return orderMapper.ToDomainModel(response.Models[0]);
    }


    private OrderMapper orderMapper;

    private readonly Client client;
}