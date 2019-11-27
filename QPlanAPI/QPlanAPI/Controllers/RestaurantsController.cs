using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QPlanAPI.Config;
using QPlanAPI.Core.DTO.Restaurants;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Domain;
using QPlanAPI.Presenters;

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

        // GET api/restaurants
        [HttpGet]
        public async Task<ActionResult> Get(int page = 0, int pageSize = 10)
        {
            var pagedRequest = new PagedRequest { Page = page, PageSize = pageSize };
            await _getAllRestaurants.Handle(new GetRestaurantsRequest() {  PagedRequest = pagedRequest }, _restaurantPresenter);
            return _restaurantPresenter.Result;
        }

        // GET api/restaurants/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        [HttpGet("location")]
        public async Task<ActionResult> Get(double longitude, double latitude, double radius, int page = 0, int pageSize = 10)
        {
                await _getRestaurantsByLocation.Handle(new GetRestaurantsByLocationRequest
            {
                Location = new Location(longitude, latitude),
                Radius = radius <= 0 ? DEFAULT_RADIUS_MAX_DISTANCE : radius,
                PagedRequest = new PagedRequest {Page = page , PageSize = pageSize }
            }, _restaurantPresenter);

            return _restaurantPresenter.Result;
        }

        // POST api/restaurants
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/restaurants/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/restaurants/5
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
            TestVips();
            TestLaSureña();
            TestMontaditos();
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
                FileContent = GetLocalRestaurantContent("vips.json")
            }, typeof(VipsRestaurantsResponse[]));
        }

        [HttpGet("test/lasurena")]
        public void TestLaSureña()
        {
            _restaurantsLocalFeeder.Handle(new FeedLocalRestaurantsRequest
            {
                FileContent = GetLocalRestaurantContent("lasurena.json")
            }, typeof(LaSurenaRestaurantsResponse[]));
        }

        [HttpGet("test/montaditos")]
        public void TestMontaditos()
        {
            _restaurantsLocalFeeder.Handle(new FeedLocalRestaurantsRequest
            {
                FileContent = GetLocalRestaurantContent("montaditos.json")
            }, typeof(MontaditosRestaurantsResponse[]));
        }

        #endregion Public Methods

        #region Private Methods

        private string GetLocalRestaurantContent(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Config", "LocalRestaurants", fileName);
            string content = string.Empty;

            using (StreamReader reader = new StreamReader(filePath))
                content = reader.ReadToEnd();

            return content;
        }

        #endregion Private Methods
    }
}