using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;
using QPlanAPI.Presenters;
using QPlanAPI.Infrastructure.Services.RestaurantsFeeder;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Config;
using Microsoft.Extensions.Options;

namespace QPlanAPI.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly RestaurantPresenter _restaurantPresenter;
        private readonly IGetAllRestaurantsUseCase _getAllRestaurants;
        private readonly IGetRestaurantsByLocationUseCase _getRestaurantsByLocation;
        private readonly IRestaurantsFeederService<FeedApiRestaurantsRequest> _restaurantsFeeder;

        private readonly ExternalRestaurantsConfig _externalRestaurantsConfig;

        private const double DEFAULT_RADIUS_MAX_DISTANCE = 1000;


        public RestaurantsController(IGetAllRestaurantsUseCase getAllRestaurants, IGetRestaurantsByLocationUseCase getRestaurantsByLocation,
         RestaurantPresenter restaurantPresenter, IRestaurantsFeederService<FeedApiRestaurantsRequest> restaurantsFeeder, IOptions<ExternalRestaurantsConfig> externalRestaurantsConfig)
        {
            _getAllRestaurants = getAllRestaurants;
            _getRestaurantsByLocation = getRestaurantsByLocation;
            _restaurantPresenter = restaurantPresenter;
            _restaurantsFeeder = restaurantsFeeder;
            _externalRestaurantsConfig = externalRestaurantsConfig.Value;
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

        [HttpGet("test/all")]
        public void UpdateRestaurantsInfo()
        {
            TestMcDonalds();
            TestKFC();
            TestFosters();
            TestGinos();
            TestTacoBell();
            TestPapaJohns();
            TestTGB();
        }

        [HttpGet("test/mcdonalds")]
        public void TestMcDonalds()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.McDonalds));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(McDonaldsRestaurantsResponse[]));
        }

        [HttpGet("test/kfc")]
        public void TestKFC()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.KFC));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(KFCRestaurantsResponse));
        }

        [HttpGet("test/fosters")]
        public void TestFosters()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.FostersHollywood));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(FostersRestaurantsResponse[]));
        }

        [HttpGet("test/ginos")]
        public void TestGinos()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.Ginos));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(GinosRestaurantsResponse));
        }

        [HttpGet("test/tacobell")]
        public void TestTacoBell()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.TacoBell));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(TacoBellRestaurantsResponse[]));
        }

        [HttpGet("test/papajohns")]
        public void TestPapaJohns()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.PapaJohns));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(PapaJohnsRestaurantsResponse));
        }

        [HttpGet("test/tgb")]
        public void TestTGB()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.TGB));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(TGBRestaurantsResponse));
        }

        [HttpGet("test/subway")]
        public void TestSubway()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.Subway));
            _restaurantsFeeder.Handle(new FeedApiRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(SubwayRestaurantsResponse));
        }

        [HttpGet("test/dominospizza")]
        public void TestDominosPizza()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.DominosPizza));
            //_restaurantsFeeder.HandleAsync(new FeedHtmlRestaurantsRequest { ApiEndpoints = config.Endpoints }, typeof(FeedRestaurantsResponse));
        }
    }
}
