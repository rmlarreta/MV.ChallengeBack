using MediatR;
using WeatherRequest.Application.Queries;
using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Domain.Interfaces;

namespace WeatherRequest.Application.QueryHandlers
{
    public class GetWeatherReportHandler : IRequestHandler<GetWeatherReport, List<WeatherRequestCity>>
    {
        private readonly ILogger<GetWeatherReportHandler> _logger;
        private readonly IWeatherService _weatherService;
        private readonly IConfiguration _configuration;

        public GetWeatherReportHandler(ILogger<GetWeatherReportHandler> logger, IWeatherService weatherService, IConfiguration configuration)
        {
            _logger = logger;
            _weatherService = weatherService;
            _configuration = configuration;
        }

        public async Task<List<WeatherRequestCity>> Handle(GetWeatherReport request, CancellationToken cancellationToken)
        {
            var result = await _weatherService.GetWeatherReport(request.GetWeatherRequest.Ciudad, request.GetWeatherRequest.Historial, _configuration["APPID"]!);
            return result;
        }
    }
}
