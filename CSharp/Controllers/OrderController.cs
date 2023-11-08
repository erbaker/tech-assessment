using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using CSharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public string GetAllOrders()
        {
            string returnString = "";
            List <Orders> orders = _orderService.GetAllOrders();
            foreach(Orders order in orders)
            {
                returnString += order.ToString() + "\n";
            }
            return returnString;
        }

        [HttpPost]
        [Route("createOrder")]
        public int CreateOrder()
        {
            return 0;
        }

        [HttpGet]
        [Route("{customerId}")]
        public string GetOrdersByCustomer(string customerId)
        {
            return "Customer ID: " + customerId;
        }

        [HttpPost]
        [Route("updateOrder/{orderId}")]
        public string UpdateOrder(string orderId)
        {
            return "";
        }

        [HttpPost]
        [Route("cancelOrder/{orderId}")]
        public string CancelOrder(string orderId)
        {
            return "";
        }
    }
}
