using System.Collections.Generic;

namespace CSharp.Services.Models
{
    public interface IOrdersService
    {
        List<Order> GetOrders();
        Order CreateOrder();
    }
}
