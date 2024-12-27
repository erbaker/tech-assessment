using CSharp.Models;
using System.Collections.Generic;

namespace CSharp.Services
{
    public interface IOrderService
    {
        Order CreateOrder(Order newOrder);
        List<Order> GetOrdersByCustomer(int customerId);
        Order UpdateOrder(int id, Order updatedOrder);
        bool CancelOrder(int id);
        Order GetOrder(int id);
    }
}
