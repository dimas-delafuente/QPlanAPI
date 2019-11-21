using System;
using QPlanAPI.Core.Interfaces.UseCases;

namespace QPlanAPI.Core.DTO.Restaurants
{
    public class GetRestaurantsRequest : IUseCaseRequest<GetRestaurantsResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

    }
}
