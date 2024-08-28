using System.Diagnostics.CodeAnalysis;

namespace CSharp.Accessors.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class OrderAccesorTests
    {
        [Test]
        public void GetOrders_ExistingCustomer_ShouldReturnListOfOrders()
        {
            // Arrange
            var orderAccessor = new OrderAccessor();

            // Act
            var orders = orderAccessor
                .GetOrders(1);

            // Assert
            Assert.That(orders.Count, Is.EqualTo(2));
        }

        [Test]
        public void GetOrders_NonExistingCustomer_ShouldReturnEmptyList()
        {
            // Arrange
            var orderAccessor = new OrderAccessor();

            // Act
            var orders = orderAccessor
                .GetOrders(100);

            // Assert
            Assert.That(orders, Is.Empty);
        }
    }
}
