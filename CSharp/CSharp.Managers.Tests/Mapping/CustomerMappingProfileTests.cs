using AutoMapper;
using CSharp.Managers.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace CSharp.Managers.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CustomerMappingProfileTests
    {
        private IMapper Mapper { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(typeof(AddressMappingProfile));
                configuration.AddProfile(typeof(CustomerMappingProfile));
            });

            Mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void CustomerMappingProfile_AssertConfigurationIsValid()
        {
            // Act & Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Customer_ViewModel_To_DataTransferObject_ShouldMap()
        {
            // Arrange
            const string FirstName = "Yoshi";
            const string LastName = "Cat";
            const int Age = 8;
            const string PhoneNumber = "9876543210";

            var customerViewModel = new ViewModels.Customer
            {
                Address = new ViewModels.Address(),
                Age = Age,
                FirstName = FirstName,
                LastName = LastName,
                PhoneNumber = PhoneNumber
            };

            // Act
            var customerDataTransferObject = Mapper.Map<Accessors.DataTransferObjects.Customer>(customerViewModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(customerDataTransferObject.Address, Is.Not.Null);
                Assert.That(customerDataTransferObject.Age, Is.EqualTo(Age));
                Assert.That(customerDataTransferObject.FirstName, Is.EqualTo(FirstName));
                Assert.That(customerDataTransferObject.LastName, Is.EqualTo(LastName));
                Assert.That(customerDataTransferObject.PhoneNumber, Is.EqualTo(PhoneNumber));
            });
        }
    }
}
