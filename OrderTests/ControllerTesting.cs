using Castle.Core.Resource;
using CSharp.Controllers;
using CSharp.Models;
using CSharp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Newtonsoft.Json;


namespace OrderTests
{
    internal class ControllerTesting
    {
        [Test]
        public async Task ControllerUpdateOrder_Success()
        {
            // Arrange
            // new OrderDetail{ OrderDetailID = 1, CustomerId = 1, OrderID = 1234, Quantity = 50, IsOrderActive = true, CreateDate = DateTime.Now, UpdateDate = DateTime.Now },
            var customerId = 1;
            var orderId = 1234;
            var quantity = 17;

            var updatedOrder = new OrderDetail
            { 
                OrderDetailID = 1, 
                CustomerId = 1, 
                OrderID = 1234, 
                Quantity = 17, 
                IsOrderActive = true, 
                CreateDate = DateTime.Now, 
                UpdateDate = DateTime.Now 
            };

            var mockOrderSrv = new Mock<IOrderDetailService>();
            mockOrderSrv.Setup(o => o.UpdateOrder(customerId, orderId, quantity))
                .ReturnsAsync(updatedOrder)
                .Verifiable();
            var controller = new Order(mockOrderSrv.Object);

            var result = controller.UpdateOrder(customerId, orderId, quantity);
            var orderResult = JsonConvert.DeserializeObject<OrderDetail>(result);

            // Assert
            Assert.AreEqual(orderResult.OrderID, updatedOrder.OrderID);
            Assert.AreEqual(orderResult.CustomerId, updatedOrder.CustomerId);
            Assert.AreEqual(orderResult.Quantity, updatedOrder.Quantity);
        }

        [Test]
        public async Task ControllerCreateOrder_Success()
        {
            // Arrange

            var newOrder = new OrderDetail
            {
                OrderDetailID = 5,
                CustomerId = 5,
                OrderID = 1255,
                Quantity = 17,
            };
            var returnedOrder = new OrderDetail
            {
                OrderDetailID = 5,
                CustomerId = 5,
                OrderID = 1255,
                Quantity = 17,
            };
            // Act
            var mockOrderSrv = new Mock<IOrderDetailService>();
            mockOrderSrv.Setup(o => o.CreateOrder(newOrder))
                .ReturnsAsync(returnedOrder)
                .Verifiable();
            var controller = new Order(mockOrderSrv.Object);

            var result = controller.CreateNewOrder(newOrder);
            var orderResult = JsonConvert.DeserializeObject<OrderDetail>(result);
            // Assert
            Assert.AreEqual(newOrder.Quantity, returnedOrder.Quantity);
            Assert.AreEqual(newOrder.CustomerId, returnedOrder.CustomerId);
            Assert.AreEqual(newOrder.OrderID, returnedOrder.OrderID);
        }

        [Test]
        public async Task ControllerCreateOrder_Exception()
        {
            // Arrange

            var newOrder = new OrderDetail
            {
                OrderDetailID = 0,
                CustomerId = 0,
                OrderID = 1255,
                Quantity = 17,
            };

            var returnedOrder = new OrderDetail
            {
                OrderDetailID = 5,
                CustomerId = 5,
                OrderID = 1255,
                Quantity = 17,
            };

            var errMsg = "Failed to Create new order. Customer 0 for order: 1255 with exception: Neither CustomerId or OrderId can be zero.";

            // Act
            var mockOrderSrv = new Mock<IOrderDetailService>();
            var controller = new Order(mockOrderSrv.Object);

            var result = controller.CreateNewOrder(newOrder);
            // Assert
            Assert.AreEqual(result, errMsg);
        }

        [Test]
        public async Task ControllerGetAll_Success()
        {
            // Arrange
            var custId = 2;

            var listOfOrders = new List<OrderDetail>()
            {
                new OrderDetail
                {
                    OrderDetailID = 2,
                    CustomerId = 2,
                    OrderID = 1244,
                    Quantity = 10,
                    IsOrderActive = true,
                    CreateDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                }
            };

            var mockOrderSrv = new Mock<IOrderDetailService>();
            mockOrderSrv.Setup(o => o.GetAllOrdersByCustomer(custId))
                .ReturnsAsync(listOfOrders)
                .Verifiable();
            var controller = new Order(mockOrderSrv.Object);

            // Act
            var result = controller.GetAllOrders(custId);
            var orderResult = JsonConvert.DeserializeObject<List<OrderDetail>>(result);

            Assert.AreEqual(orderResult[0].OrderID, listOfOrders[0].OrderID);
            Assert.IsNotNull(result);
        }

        [Test]
        public async Task ControllerGetAllOrdersZero_Success()
        {
            // Arrange
            var custId = 0;
            var returnMsg = "CustomerId must be Greater than zero.";

            var listOfOrders = new List<OrderDetail>();
            var mockOrderSrv = new Mock<IOrderDetailService>();
            mockOrderSrv.Setup(o => o.GetAllOrdersByCustomer(custId))
                .ReturnsAsync(listOfOrders)
                .Verifiable();
            var controller = new Order(mockOrderSrv.Object);

            // Act
            var result = controller.GetAllOrders(custId);

            // Assert
            Assert.AreEqual(returnMsg, result);
        }

        [Test]
        public async Task ControllerCancel_Success()
        {
            // Arrange
            var custId = 2;
            var orderId = 1244;

            var cancelledOrder = new OrderDetail
            {
                OrderDetailID = 2,
                CustomerId = 2,
                OrderID = 1244,
                Quantity = 10,
                IsOrderActive = false,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now
            };

            var mockOrderSrv = new Mock<IOrderDetailService>();

            mockOrderSrv.Setup(o => o.CancelOrder(custId, orderId))
             .ReturnsAsync(cancelledOrder)
             .Verifiable();

            var controller = new Order(mockOrderSrv.Object);
            var result = controller.CancelOrder(custId, orderId);
            var orderResult = JsonConvert.DeserializeObject<OrderDetail>(result);
            // Assert
            Assert.AreEqual(custId, orderResult.CustomerId);
            Assert.AreEqual(orderId, orderResult.OrderID);
            Assert.AreEqual(false, orderResult.IsOrderActive);
        }
    }
}
