using System;
using System.Threading.Tasks;

namespace QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder
{
    public interface IRestaurantsFeederService<in TFeedRestaurantsRequest>
        where TFeedRestaurantsRequest : FeedRestaurantsRequest
    {
        Task<bool> Handle(TFeedRestaurantsRequest request, Type response);
    }
}