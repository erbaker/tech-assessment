using System.Collections.Generic;
using CSharp.Services.Models;
using CSharp.Services;
using Moq;
using NUnit.Framework;

namespace OrdersControllerTests
{
    public class OrdersConrollerTests
    {
        private OrdersController _sut;
        private Mock<IOrdersService> _ordersServiceMock;

        [SetUp]
        public void Setup()
        {
            _ordersServiceMock = new Mock<IOrdersService>();
            _ordersServiceMock.Setup(x => x.GetOrders()).Returns(new List<Order>() { new Order() });

            _sut = new OrdersController(_ordersServiceMock.Object);

        }

        [Test]
        public void WhenGetOrdersThenReturnPopulatedList()
        {
            Assert.IsTrue(_sut.GetOrders().Count > 0);
        }

        [Test]
        public void WhenCreateOrderThenReturnNewOrder()
        {
            var order = new Order();
            // Mock out what the service should return when we ask for a new order
            _ordersServiceMock.Setup(x => x.CreateOrder()).Returns(order);

            Assert.AreEqual(order, _sut.CreateOrder());
        }
    }
}