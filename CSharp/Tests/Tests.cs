using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Service;
using CSharp.Repository;
using Xunit;

namespace CSharp.Tests
{
    public class Tests
    {
        public OrdersService _ordersService = new OrdersService();
        public OrderRepository _orderRepository = new OrderRepository();

        [Fact]
        public void CreateOrderTest()
        {
            //arrange
            var customer = "Konsor";
            var items = new List<string>() { "pen", "paper" };

            var orderId = _ordersService.CreateOrder(customer, items);

            var newOrder = _orderRepository.orders.Where(o => o.Id == orderId).FirstOrDefault();

            Assert.Equal(customer, newOrder.Customer);
            Assert.Equal(items, newOrder.Items);
        }
    }
}
