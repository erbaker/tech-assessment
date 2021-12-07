using CSharp.Controllers;
using CSharp.Services;
using NUnit.Framework;

namespace ControllerTests
{
    public class ControllerTests
    {
        OrdersController sut;

        [SetUp]
        public void Setup()
        {
            sut = new OrdersController(new OrdersService());

        }

        [Test]
        public void WhenGetOrdersThenReturnPopulatedList()
        {
            Assert.IsTrue(sut.GetOrders().Count > 0);
        }
    }
}