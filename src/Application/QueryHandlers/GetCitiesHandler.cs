using MediatR;
using WeatherRequest.Application.Queries;
using WeatherRequest.Domain.AggregateRoot.Values;
using WeatherRequest.Domain.Interfaces;

namespace WeatherRequest.Application.QueryHandlers
{
    public class GetCitiesHandler : IRequestHandler<GetCities, List<Ciudad>>
    {
        private readonly ILogger<GetCitiesHandler> _logger;
        private readonly ICitiesService _citiesService;

        public GetCitiesHandler(ILogger<GetCitiesHandler> logger, ICitiesService citiesService)
        {
            _logger = logger;
            _citiesService = citiesService;
        }

        public async Task<List<Ciudad>> Handle(GetCities request, CancellationToken cancellationToken)
        {
            return await _citiesService.GetAllCities();
        }
    }
}
