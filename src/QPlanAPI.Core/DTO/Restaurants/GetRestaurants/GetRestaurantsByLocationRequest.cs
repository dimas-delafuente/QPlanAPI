using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class GetRestaurantsByLocationRequest : IUseCaseRequest<GetRestaurantsResponse>
    {
        public Location Location { get; set; }

        public PagedRequest PagedRequest { get; set; }

        public double Radius { get; set; }
    }
}
