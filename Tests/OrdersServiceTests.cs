using CSharp.Services;
using CSharp.Services;
using NUnit.Framework;

namespace OrdersServiceTests
{
    public class OrdersServiceTests
    {
        OrdersService sut;

        [SetUp]
        public void Setup()
        {
            sut = new OrdersService();

        }

        [Test]
        public void WhenGetOrdersThenReturnPopulatedList()
        {
            Assert.IsTrue(sut.GetOrders().Count > 0);
        }
    }
}