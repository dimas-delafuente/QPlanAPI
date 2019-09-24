using System.Collections.Generic;
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
    public class GetAllRestaurantsUseCaseUnitTest
    {
        private readonly Mock<IRestaurantRepository> _restaurantRepository;
        private readonly Mock<IOutputPort<GetRestaurantsResponse>> _mockOutputPort;
        public GetAllRestaurantsUseCaseUnitTest()
        {
            _mockOutputPort = new Mock<IOutputPort<GetRestaurantsResponse>>();
            _restaurantRepository = new Mock<IRestaurantRepository>();
        }

        [Fact]
        public async void Can_Get_Restaurants()
        {

            _restaurantRepository
              .Setup(repo => repo.GetAllRestaurants())
              .Returns(Task.FromResult(GetAllRestaurantsSample()));

            var useCase = new GetAllRestaurantsUseCase(_restaurantRepository.Object);

            _mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<GetRestaurantsResponse>()));

            var response = await useCase.Handle(new GetRestaurantsRequest(), _mockOutputPort.Object);

            Assert.True(response);
        }

        public IEnumerable<Restaurant> GetAllRestaurantsSample()
        {
            return new List<Restaurant>{
                new Restaurant{
                    Id = "5d682f889fb0cb2afacb57cc",
                    Name = "La Centinela"
                }
            };
        }

    }
}