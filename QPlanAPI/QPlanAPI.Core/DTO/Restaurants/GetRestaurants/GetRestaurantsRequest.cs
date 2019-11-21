using System;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class GetRestaurantsRequest : IUseCaseRequest<GetRestaurantsResponse>
    {
        public PagedRequest PagedRequest { get; set; }
    }
}
