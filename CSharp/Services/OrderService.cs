using CSharp.Models;
using CSharp.Repository;
using System.Collections.Generic;

namespace CSharp.Services 
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Orders> GetAllOrders()
        {
            return _orderRepository.GetAllOrdersAsync().Result;
        }
    }

    public interface IOrderService
    {
        List<Orders> GetAllOrders();
    }
}