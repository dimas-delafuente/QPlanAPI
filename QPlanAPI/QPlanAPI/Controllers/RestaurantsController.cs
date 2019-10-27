using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;
using QPlanAPI.Presenters;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Config;
using Microsoft.Extensions.Options;

namespace QPlanAPI.Controllers
{
    [Route("api/restaurants")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        #region Constants
        private const double DEFAULT_RADIUS_MAX_DISTANCE = 1000;
        #endregion Constants

        #region Properties
        private readonly RestaurantPresenter _restaurantPresenter;
        private readonly IGetAllRestaurantsUseCase _getAllRestaurants;
        private readonly IGetRestaurantsByLocationUseCase _getRestaurantsByLocation;
        private readonly IRestaurantsFeederService<FeedApiRestaurantsRequest> _restaurantsApiFeeder;
        private readonly IRestaurantsFeederService<FeedHtmlRestaurantsRequest> _restaurantsHtmlFeeder;
        private readonly IRestaurantsFeederService<FeedLocalRestaurantsRequest> _restaurantsLocalFeeder;
        private readonly ExternalRestaurantsConfig _externalRestaurantsConfig;
        #endregion Properties

        #region Public Methods

        public RestaurantsController(IGetAllRestaurantsUseCase getAllRestaurants, IGetRestaurantsByLocationUseCase getRestaurantsByLocation,
         RestaurantPresenter restaurantPresenter, IRestaurantsFeederService<FeedApiRestaurantsRequest> restaurantsApiFeeder,
         IRestaurantsFeederService<FeedHtmlRestaurantsRequest> restaurantsHtmlFeeder, IRestaurantsFeederService<FeedLocalRestaurantsRequest> restaurantsLocalFeeder,
         IOptions<ExternalRestaurantsConfig> externalRestaurantsConfig)
        {
            _getAllRestaurants = getAllRestaurants;
            _getRestaurantsByLocation = getRestaurantsByLocation;
            _restaurantPresenter = restaurantPresenter;
            _restaurantsApiFeeder = restaurantsApiFeeder;
            _restaurantsHtmlFeeder = restaurantsHtmlFeeder;
            _restaurantsLocalFeeder = restaurantsLocalFeeder;
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
            TestDominosPizza();
            TestSubway();
        }

        [HttpGet("test/mcdonalds")]
        public void TestMcDonalds()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.McDonalds));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(McDonaldsRestaurantsResponse[]));
        }

        [HttpGet("test/kfc")]
        public void TestKFC()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.KFC));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(KFCRestaurantsResponse));
        }

        [HttpGet("test/fosters")]
        public void TestFosters()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.FostersHollywood));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(FostersRestaurantsResponse[]));
        }

        [HttpGet("test/ginos")]
        public void TestGinos()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.Ginos));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(GinosRestaurantsResponse));
        }

        [HttpGet("test/tacobell")]
        public void TestTacoBell()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.TacoBell));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(TacoBellRestaurantsResponse[]));
        }

        [HttpGet("test/papajohns")]
        public void TestPapaJohns()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.PapaJohns));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(PapaJohnsRestaurantsResponse));
        }

        [HttpGet("test/tgb")]
        public void TestTGB()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.TGB));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(TGBRestaurantsResponse));
        }

        [HttpGet("test/subway")]
        public void TestSubway()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.Subway));
            _restaurantsApiFeeder.Handle(new FeedApiRestaurantsRequest
            {
                Endpoints = config.Endpoints,
                ApiFormat = config.Format
            }, typeof(SubwayRestaurantsResponse));
        }

        [HttpGet("test/dominospizza")]
        public void TestDominosPizza()
        {
            var config = _externalRestaurantsConfig.ApiRestaurants.FirstOrDefault(x => x.Type.Equals(RestaurantType.DominosPizza));
            _restaurantsHtmlFeeder.Handle(new FeedHtmlRestaurantsRequest
            {
                Endpoints = config.Endpoints
            }, typeof(DominosPizzaRestaurantsResponse));
        }

        [HttpGet("test/vips")]
        public void TestVips()
        {
            _restaurantsLocalFeeder.Handle(new FeedLocalRestaurantsRequest
            {
                FileName = "vips.json"
            }, typeof(VipsRestaurantsResponse));;
        }

        [HttpGet("test/lasureña")]
        public void TestLaSureña()
        {
            _restaurantsLocalFeeder.Handle(new FeedLocalRestaurantsRequest
            {
                FileName = "lasureña.json"
            }, typeof(LaSureñaRestaurantsResponse));
        }

        [HttpGet("test/montaditos")]
        public void TestMontaditos()
        {
            _restaurantsLocalFeeder.Handle(new FeedLocalRestaurantsRequest
            {
                FileName = "montaditos.json"
            }, typeof(MontaditosRestaurantsResponse));
        }

        #endregion Public Methods
    }
}