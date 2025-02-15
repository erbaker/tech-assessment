using CSharp.Models;
using CSharp.Services;

namespace OrderTests
{
    public class Tests
    {
        [Test]
        public void GetAllOrdersByCustomer_Success()
        {
       
            OrderDetailService orderService = new OrderDetailService();
            var getExistingOrders = orderService.GetAllOrdersByCustomer(1).Result;
            var customerCount = getExistingOrders.Count();
            var expectedCount = 1;

            Assert.IsNotNull(getExistingOrders);
            Assert.IsTrue(customerCount == expectedCount);
        }

        [Test]
        public void CreateOrder_Success()
        {
            var newOrder = new OrderDetail
            {
                OrderDetailID = 5,
                OrderID = 5,
                IsOrderActive = true,
                CreateDate = DateTime.Now,
                UpdateDate = DateTime.Now,
                CustomerId = 3,
                Quantity = 100,
            };

            OrderDetailService orderService = new OrderDetailService();
            var getExistingOrders = orderService.GetAllOrdersByCustomer(newOrder.CustomerId).Result;
            var beforeCount = getExistingOrders.Count();

            var ordInfo = orderService.CreateOrder(newOrder).Result;
            
            Assert.IsNotNull(ordInfo);
            Assert.IsTrue(ordInfo.OrderDetailID > beforeCount);
        }

        [Test]
        public void UpdateOrder_Success()
        {
            OrderDetailService orderService = new OrderDetailService();
            var updatedOrders = orderService.UpdateOrder(1, 1234, 15).Result;

            Assert.IsNotNull(updatedOrders);
            Assert.IsTrue(updatedOrders.Quantity == 15);

        }

        [Test]
        public void CancelOrder_Success()
        {
            OrderDetailService orderService = new OrderDetailService();
            var cancelledOrder = orderService.CancelOrder(1, 1234).Result;

            Assert.IsNotNull(cancelledOrder);
            Assert.IsTrue(cancelledOrder.IsOrderActive == false);

        }
    }
}