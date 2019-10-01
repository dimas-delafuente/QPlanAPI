using Microsoft.Extensions.DependencyInjection;
using QPlanAPI.Infrastructure.Services.RestaurantsFeeder;
using QPlanAPI.Core.Interfaces.Services.RestaurantsFeeder;

namespace QPlanAPI.Infrastructure
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationInfrastructure(this IServiceCollection services)
        {
            
            services.AddScoped<IRestaurantsFeederService<FeedApiRestaurantsRequest>, ApiRestaurantsFeeder>();
            services.AddScoped<IRestaurantsFeederService<FeedHtmlRestaurantsRequest>, HtmlRestaurantsFeeder>();
        }
    }
}
