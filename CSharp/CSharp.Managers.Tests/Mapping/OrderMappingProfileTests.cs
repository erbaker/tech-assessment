using AutoMapper;
using CSharp.Managers.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace CSharp.Managers.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class OrderMappingProfileTests
    {
        private IMapper Mapper { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(typeof(AddressMappingProfile));
                configuration.AddProfile(typeof(CustomerMappingProfile));
                configuration.AddProfile(typeof(OrderMappingProfile));
            });

            Mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void OrderMappingProfile_AssertConfigurationIsValid()
        {
            // Act & Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Order_ViewModel_To_DataTransferObject_ShouldMap()
        {
            // Arrange
            const decimal OrderTotal = 100.00m;

            var orderViewModel = new ViewModels.Order
            {
                OrderTotal = OrderTotal,
                Customer = new ViewModels.Customer()
            };

            // Act
            var orderDataTransferObject = Mapper.Map<Accessors.DataTransferObjects.Order>(orderViewModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(orderDataTransferObject.OrderTotal, Is.EqualTo(OrderTotal));

                Assert.That(orderDataTransferObject.Customer, Is.Not.Null);
            });
        }

        [Test]
        public void Order_DataTransferObject_To_ViewModel_ShouldMap()
        {
            // Arrange
            const decimal OrderTotal = 100.00m;

            var orderDataTransferObject = new Accessors.DataTransferObjects.Order
            {
                OrderTotal = OrderTotal,
                Customer = new Accessors.DataTransferObjects.Customer()
            };

            // Act
            var orderViewModel = Mapper.Map<ViewModels.Order>(orderDataTransferObject);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(orderViewModel.OrderTotal, Is.EqualTo(OrderTotal));

                Assert.That(orderViewModel.Customer, Is.Not.Null);
            });
        }
    }
}
