namespace CSharp.Services;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using NLog;

public interface IOrderDetailService
{
    Task<List<OrderDetail>> GetAllOrdersByCustomer(int customerId);
    Task<OrderDetail> CreateOrder(OrderDetail order);
    Task<OrderDetail> UpdateOrder(int customerId, long orderId, long quantity);
    Task<OrderDetail> CancelOrder(int customerId, long orderId);
}

public class OrderDetailService : IOrderDetailService
{
    private Logger logger = LogManager.GetCurrentClassLogger();
    // building data for the table
    private List<OrderDetail> _orderDetails = new List<OrderDetail>
    {
        new OrderDetail{ OrderDetailID = 1, CustomerId = 1, OrderID = 1234, Quantity = 50, IsOrderActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new OrderDetail{ OrderDetailID = 2, CustomerId = 2, OrderID = 1244, Quantity = 10,IsOrderActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new OrderDetail{ OrderDetailID = 3, CustomerId = 3, OrderID = 1254, Quantity = 15,IsOrderActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
        new OrderDetail{ OrderDetailID = 4, CustomerId = 3, OrderID = 1264, Quantity = 3, IsOrderActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
    };

    public async Task<List<OrderDetail>> GetAllOrdersByCustomer(int customerId)
    {
        var ordersByCust = await Task.Run(() => _orderDetails.Where(x => x.CustomerId == customerId).ToList());
        return ordersByCust;
    }

    public async Task<OrderDetail> CreateOrder(OrderDetail orderDetail)
    {
        try
        { 
            var newOrder = new OrderDetail
            {
                OrderDetailID = orderDetail.OrderDetailID,
                CustomerId = orderDetail.CustomerId,
                OrderID = orderDetail.OrderID,
                IsOrderActive = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
            };
            _orderDetails.Add(newOrder);
            //savechanges
            return newOrder; 
        }
        catch (Exception ex)
        {
            logger.Error($"Failed to Create order {orderDetail.OrderID} for {orderDetail.CustomerId}. Error: {ex.Message}");
            return new OrderDetail();
        }
    }

    public async Task<OrderDetail> UpdateOrder(int customerId, long orderID, long quantity)
    {
        try
        {
            var updatedOrder = _orderDetails.Where(x => x.OrderID == orderID && x.CustomerId == customerId).FirstOrDefault();
            updatedOrder.Quantity = quantity;
            updatedOrder.UpdateDate = DateTime.Now;
            return updatedOrder;
        }
        catch (Exception ex)
        {
            logger.Error($"Failed to Update order {orderID} for {customerId}.Error: {ex.Message}");
            return new OrderDetail();
        }
    }

    public async Task<OrderDetail> CancelOrder(int customerId, long orderId)
    {
        try
        {
            var fndOrder = _orderDetails.Where(x => x.OrderID == orderId && x.CustomerId == customerId).FirstOrDefault();
            fndOrder.IsOrderActive = false;
            fndOrder.UpdateDate = DateTime.Now;

            return fndOrder;
        }
        catch (Exception ex)
        {
            logger.Error($"Failed to Cancel order {orderId} for {customerId}. Error: {ex.Message}");
            return new OrderDetail();
        }
    }
}
