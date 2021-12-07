using CSharp.Services.Models;
using System.Collections.Generic;

namespace CSharp.Services
{
    public class OrdersService : IOrdersService
    {

        List<Order> orders;

        // return list of orders
        public OrdersService() 
        {
            
        }

        public List<Order> GetOrders() 
        {
            orders = new List<Order>() { new Order() };

            return orders;
        } 

    }
}
