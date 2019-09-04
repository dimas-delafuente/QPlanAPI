using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;
using QPlanAPI.Presenters;

namespace QPlanAPI.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantPresenter _restaurantPresenter;
        private readonly IGetAllRestaurantsUseCase _getAllRestaurants;
        private readonly IGetRestaurantsByLocationUseCase _getRestaurantsByLocation;

        private const double DEFAULT_RADIUS_MAX_DISTANCE = 1000;


        public RestaurantsController(IGetAllRestaurantsUseCase getAllRestaurants, IGetRestaurantsByLocationUseCase getRestaurantsByLocation,
         RestaurantPresenter restaurantPresenter)
        {
            _getAllRestaurants = getAllRestaurants;
            _getRestaurantsByLocation = getRestaurantsByLocation;
            _restaurantPresenter = restaurantPresenter;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            await _getAllRestaurants.Handle(new GetRestaurantsRequest(), _restaurantPresenter);
            return _restaurantPresenter.Result;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("location")]
        public async Task<ActionResult> Get(double longitude, double latitude, double radius)
        {
            await _getRestaurantsByLocation.Handle(new GetRestaurantsByLocationRequest
            {
                Location = new Location(longitude, latitude),
                Radius = radius <= 0 ? DEFAULT_RADIUS_MAX_DISTANCE : radius
            }, _restaurantPresenter);

            return _restaurantPresenter.Result;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
