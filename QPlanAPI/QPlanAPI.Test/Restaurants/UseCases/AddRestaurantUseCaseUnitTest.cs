using System.Threading.Tasks;
using Moq;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Core.UseCases;
using QPlanAPI.Domain.Restaurants;
using Xunit;

namespace QPlanAPI.Test.Restaurants
{
    public class AddRestaurantUseCaseUnitTest
    {
        private readonly Mock<IRestaurantRepository> _restaurantRepository;
        private readonly Mock<IOutputPort<AddRestaurantResponse>> _mockOutputPort;
        public AddRestaurantUseCaseUnitTest()
        {
            _mockOutputPort = new Mock<IOutputPort<AddRestaurantResponse>>();
            _restaurantRepository = new Mock<IRestaurantRepository>();
        }

        [Fact]
        public async void Can_Add_Restaurant()
        {
            _restaurantRepository
              .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
              .Returns(Task.FromResult(true));

            var useCase = new AddRestaurantUseCase(_restaurantRepository.Object);

            _mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<AddRestaurantResponse>()));

            var response = await useCase.Handle(new AddRestaurantRequest(), _mockOutputPort.Object);

            Assert.True(response);
        }
    }
}
