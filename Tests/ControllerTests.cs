using CSharp.Controllers;
using NUnit.Framework;

namespace ControllerTests
{
    public class ControllerTests
    {
        OrdersController sut;

        [SetUp]
        public void Setup()
        {
            sut = new OrdersController();


        }

        [Test]
        public void WhenGetOrdersThenReturnPopulatedList()
        {
            Assert.IsTrue(sut.GetOrders().Count > 0);
        }
    }
}