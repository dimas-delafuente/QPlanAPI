﻿using System.Threading.Tasks;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Repositories;
using QPlanAPI.Core.Interfaces.UseCases;
using System.Linq;
using System.Collections.Generic;
using QPlanAPI.Domain.Restaurants;

namespace QPlanAPI.Core.UseCases
{
    public class GetRestaurantsByLocationUseCase : IGetRestaurantsByLocationUseCase
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public GetRestaurantsByLocationUseCase(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task<bool> Handle(GetRestaurantsByLocationRequest request, IOutputPort<GetRestaurantsResponse> outputPort)
        {
            IEnumerable<Restaurant> response = await _restaurantRepository.GetPagedRestaurantsByLocation(request.Location, request.Radius, request.PagedRequest);
            outputPort.Handle(response.Any() ? new GetRestaurantsResponse(response.ToList(), true, string.Empty) : new GetRestaurantsResponse(new List<Restaurant>(), false, string.Empty));
            return response.Any();
        }
    }
}
