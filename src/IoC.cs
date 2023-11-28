using System.Reflection;
using WeatherRequest.Domain.Interfaces;
using WeatherRequest.Infraestructure.Entities;
using WeatherRequest.Infraestructure.Interfaces;
using WeatherRequest.Infraestructure.Services;
using WeatherRequest.Infraestructure.UnitOfWorks;
using WeatherRequest.Infrastructure.Data.Repositories;
using WeatherRequest.Infrastructure.Data.Services;
using WeatherRequest.Infrastructure.UnitOfWorks;

namespace WeatherRequest.Api.Web
{
    public static class IoC
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        { 

            #region Commons
            services.AddMediatR(_=>_.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            #endregion

            #region Service
            services.AddMemoryCache();
            services.AddScoped<ICitiesService, CitiesService>();
            services.AddScoped<ICacheService, CacheService>();

            services.AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                client.BaseAddress = new Uri(configuration["UrlApiWeather"]!);   
            });
            #endregion Service

            #region Repositories
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<IEntity, Entity>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            #endregion Repositories

        }
    }
}
