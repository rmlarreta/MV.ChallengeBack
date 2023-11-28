using AutoMapper;
using WeatherRequest.Domain.AggregateRoot.Values;
using WeatherRequest.Domain.Interfaces;
using WeatherRequest.Infraestructure.Entities.Bd;
using WeatherRequest.Infraestructure.Interfaces;
using WeatherRequest.Infraestructure.UnitOfWorks;
using WeatherRequest.Infrastructure.Data.Services;

namespace WeatherRequest.Infraestructure.Services
{
    public class CitiesService : Service<CityBd>, ICitiesService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CitiesService> _logger;
        private readonly IMapper _mapper;

        public CitiesService(IUnitOfWork unitOfWork, ICacheService cacheService, ILogger<CitiesService> logger, IMapper mapper) : base(unitOfWork)
        {
            _cacheService = cacheService;
            _logger = logger;
            _mapper = mapper;
        }

        public Task<List<Ciudad>> GetAllCities()
        {
            return _cacheService.GetOrSet("Cities", async () =>
            {
                _logger.LogInformation("Trataremos de sacarlos por Cache");
                var cities = await Task.FromResult(base.GetAll().OrderBy(x=>x.Name));
                return _mapper.Map<List<Ciudad>>(cities);
            }, TimeSpan.FromDays(90));
        }
    }
}
