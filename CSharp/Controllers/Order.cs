using CSharp.Services;
using Microsoft.AspNetCore.Mvc;
using NLog;
using Newtonsoft.Json;
using System;
using CSharp.Models;

namespace CSharp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Order: ControllerBase
    {
       // Normally would use EntityFramework to define the database and the tables within.
       // private readonly OrderContext _ordersContext;
        private Logger logger = LogManager.GetCurrentClassLogger();
        private readonly IOrderDetailService _orderDetailService;

        public Order(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        [HttpGet]
        public string Test()
        {
            return "Successful Test!";
        }

        [HttpGet]
        [Route("Orders")]
        public string GetAllOrders(int customerId)
        {
            try
            {
                if (customerId == 0)
                {
                    return "CustomerId must be Greater than zero.";
                }
                var allOrdersByCustomer = _orderDetailService.GetAllOrdersByCustomer(customerId);
                var customerOrders = JsonConvert.SerializeObject(allOrdersByCustomer.Result);
                return customerOrders;
            }
            catch (Exception  ex) 
            {
                var errMsg = $"GetAllOrders failed {customerId} with exception: {ex.Message}";
                logger.Error(errMsg);
                return $"Failed to Return Orders for Customer {customerId}";
            }
        }

        [HttpPost]
        [Route("Order")]
        public string CreateNewOrder([FromBody] OrderDetail orderDetail)
        {
            try
            {
                if (orderDetail.CustomerId == 0 || orderDetail.OrderID == 0)
                {
                    throw new Exception($"Neither CustomerId or OrderId can be zero.");
                }
                var newOrder = new OrderDetail
                {
                    OrderID = orderDetail.OrderID,
                    CustomerId = orderDetail.CustomerId,
                    IsOrderActive = true,
                    Quantity = orderDetail.Quantity,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now,
                };
                var createOrder = _orderDetailService.CreateOrder(newOrder);
                var createdOrder = JsonConvert.SerializeObject(createOrder.Result);
                return createdOrder;
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to Create new order. Customer {orderDetail.CustomerId} for order: {orderDetail.OrderID} with exception: {ex.Message}";
                logger.Error(errMsg);
                return errMsg;
            }
        }

        [HttpPost]
        [Route("Order")]
        public string UpdateOrder([FromBody] int CustomerId,long OrderId, long Quantity )
        {
            try
            {
                if (CustomerId == 0 || OrderId == 0)
                {
                    new Exception($"Neither CustomerId or OrderId can be zero."); 
                }
    
                var createOrder = _orderDetailService.UpdateOrder(CustomerId, OrderId, Quantity);
                var updatedOrder = JsonConvert.SerializeObject(createOrder.Result);
                return updatedOrder;
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to Update order. Customer {CustomerId} for order: {OrderId} with exception: {ex.Message}";
                logger.Error(errMsg);
                return errMsg;
            }
        }

        [HttpPost]
        [Route("Order")]
        public string CancelOrder([FromBody] int CustomerId, long OrderId)
        {
            try
            {
                if (CustomerId == 0 || OrderId == 0)
                {
                    new Exception($"Neither CustomerId or OrderId can be zero.");
                }

                var cancelOrder = _orderDetailService.CancelOrder(CustomerId, OrderId);
                var cancelledOrder = JsonConvert.SerializeObject(cancelOrder.Result);
                return cancelledOrder;
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to Update order. Customer {CustomerId} for order: {OrderId} with exception: {ex.Message}";
                logger.Error(errMsg);
                return errMsg;
            }
        }

    }
}
