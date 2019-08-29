using Microsoft.Extensions.DependencyInjection;
using QPlanAPI.Core.Interfaces.UseCases;
using QPlanAPI.Core.UseCases;

namespace QPlanAPI.Core
{
    public static class ServiceCollectionExtension
    {
        public static void AddApplicationCore(this IServiceCollection services)
        {
            services.AddScoped<IAddRestaurantUseCase, AddRestaurantUseCase>();
            services.AddScoped<IGetAllRestaurantsUseCase, GetAllRestaurantsUseCase>();

        }
    }
}
