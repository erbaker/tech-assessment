using CSharp.Domain;
using CSharp.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Service
{
    public class OrdersService
    {
        OrderRepository _orderRepository = new OrderRepository();
        public Guid CreateOrder(string customer, List<string> items)
        {
            var newOrder = new Order()
            {
                Customer = customer,
                Items = items
            };
            _orderRepository.orders.Add(newOrder);

            return newOrder.Id;
        }

        public List<Order> GetOrdersByCustomer(string customer)
        {
            return _orderRepository.orders.Where(o => o.Customer == customer).ToList();
        }

        public void UpdateOrder(Guid orderId, List<string> items)
        {
            var orderToUpdate = _orderRepository.orders.Where(o => o.Id == orderId).FirstOrDefault();

            if (orderToUpdate != default)
            {
                orderToUpdate.Items = items;
                return;
            }
            else
            {
                return;
            }
        }

        public void CancelOrder(Guid orderId)
        {
            var orderToCancel = _orderRepository.orders.Where(o => o.Id == orderId).FirstOrDefault();

            if (orderToCancel != default)
            {
                _orderRepository.orders.Remove(orderToCancel);
                return;
            }
            else
            {
                return;
            }
        }
    }
}
