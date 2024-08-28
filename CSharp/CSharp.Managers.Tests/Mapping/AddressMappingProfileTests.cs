using AutoMapper;
using CSharp.Managers.Mapping;
using System.Diagnostics.CodeAnalysis;

namespace CSharp.Managers.Tests.Mapping
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AddressMappingProfileTests
    {
        private IMapper Mapper { get; set; }

        [SetUp]
        public void SetUp()
        {
            var mapperConfiguration = new MapperConfiguration(configuration =>
            {
                configuration.AddProfile(typeof(AddressMappingProfile));
            });

            Mapper = mapperConfiguration.CreateMapper();
        }

        [Test]
        public void AddressMappingProfile_AssetConfigurationIsValid()
        {
            // Act & Assert
            Mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Test]
        public void Address_ViewModel_To_DataTransferObject_ShouldMap()
        {
            // Arrange
            const string StreetAddress = "123 Address";
            const string City = "Lincoln";
            const string State = "NE";
            const string PostalCode = "12345";

            var addressViewModel = new ViewModels.Address
            {
                City = City,
                PostalCode = PostalCode,
                State = State,
                StreetAddress = StreetAddress
            };

            // Act
            var addressDataTransferObject = Mapper.Map<Accessors.DataTransferObjects.Address>(addressViewModel);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(addressDataTransferObject.StreetAddress, Is.EqualTo(StreetAddress));
                Assert.That(addressDataTransferObject.City, Is.EqualTo(City));
                Assert.That(addressDataTransferObject.State, Is.EqualTo(State));
                Assert.That(addressDataTransferObject.PostalCode, Is.EqualTo(PostalCode));
            });
        }

        [Test]
        public void Address_DataTransferObject_To_ViewModel_ShouldMap()
        {
            // Arrange
            const string StreetAddress = "123 Address";
            const string City = "Lincoln";
            const string State = "NE";
            const string PostalCode = "12345";

            var addressDataTransferObject = new Accessors.DataTransferObjects.Address
            {
                City = City,
                PostalCode = PostalCode,
                State = State,
                StreetAddress = StreetAddress
            };

            // Act
            var addressViewModel = Mapper.Map<ViewModels.Address>(addressDataTransferObject);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(addressViewModel.StreetAddress, Is.EqualTo(StreetAddress));
                Assert.That(addressViewModel.City, Is.EqualTo(City));
                Assert.That(addressViewModel.State, Is.EqualTo(State));
                Assert.That(addressViewModel.PostalCode, Is.EqualTo(PostalCode));
            });
        }
    }
}
