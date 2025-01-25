using CSharp.Models;

namespace CSharp.Repositories;

public interface IOrderRepository
{
    Task<Order> CreateOrderAsync(OrderCreateDto orderCreate);
    Task<IEnumerable<Order>> GetOrdersByCustomerAsync(string customerId);
    Task<Order?> GetOrderAsync(string orderId);
    Task<Order?> UpdateOrderAsync(string orderId, OrderUpdateDto orderUpdate);
    Task<Order?> CancelOrderAsync(string orderId);
}