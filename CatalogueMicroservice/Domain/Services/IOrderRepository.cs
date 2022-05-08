using ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Models;

namespace ChiraagShoeAppBackend.CatalogueMicroservice.Domain.Services;

public interface IOrderRepository
{
    Task<Order> CreateOrder(string product, string purchasedBy, float amount);
    Task<Order?> GetOrder(string product);
    Task<Order[]> GetOrdersPurchasedBy(string userId);
    Task<Order> StoreOrder(Order order);
}