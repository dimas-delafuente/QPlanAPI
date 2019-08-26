using System.Threading.Tasks;
using Moq;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Core.UseCases.Restaurants;
using QPlanAPI.Domain.Restaurants;
using Xunit;

namespace QPlanAPI.Test
{
    public class RegisterUserUseCaseUnitTests
    {

        [Fact]
        public async void Can_Add_Restaurant()
        {
            // arrange

            // 1. We need to store the user data somehow
            var mockUserRepository = new Mock<IRestaurantRepository>();
            mockUserRepository
              .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
              .Returns(Task.FromResult(new AddRestaurantResponse(true, string.Empty)));

            // 2. The use case and star of this test
            var useCase = new AddRestaurantUseCase(mockUserRepository.Object);

            // 3. The output port is the mechanism to pass response data from the use case to a Presenter 
            // for final preparation to deliver back to the UI/web page/api response etc.
            var mockOutputPort = new Mock<IOutputPort<AddRestaurantResponse>>();
            mockOutputPort.Setup(outputPort => outputPort.Handle(It.IsAny<AddRestaurantResponse>()));

            // act

            // 4. We need a request model to carry data into the use case from the upper layer (UI, Controller etc.)
            var response = await useCase.Handle(new AddRestaurantRequest(), mockOutputPort.Object);

            // assert
            Assert.True(response);
        }
    }
}
