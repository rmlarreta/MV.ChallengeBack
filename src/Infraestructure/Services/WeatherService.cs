using AutoMapper;
using System.Linq.Expressions;
using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Domain.AggregateRoot.Values;
using WeatherRequest.Domain.Interfaces;
using WeatherRequest.Infraestructure.Entities.Bd;
using WeatherRequest.Infraestructure.UnitOfWorks;
using WeatherRequest.Infrastructure.Data.Services;
using static WeatherRequest.Infraestructure.Entities.Api.WeatherReport;

namespace WeatherRequest.Infraestructure.Services
{
    public class WeatherService : Service<WeatherReportBd>, IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<WeatherService> _logger;
        private readonly IMapper _mapper;
        private const int MAXREGISTERS = 100;

        public WeatherService(IUnitOfWork unitOfWork, HttpClient httpClient, ILogger<WeatherService> logger, IMapper mapper) : base(unitOfWork)
        {
            _httpClient = httpClient;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<List<WeatherRequestCity>> GetWeatherReport(Ciudad ciudad, bool historial, string appid)//En una futura iteracón deberá inyectarse en el cliente
        {
            _logger.LogInformation("Consultado {Uri}", _httpClient.BaseAddress + ciudad.Nombre + "," + ciudad.Pais);

            var response = await _httpClient.GetAsync($"?APPID={appid}&q=" + ciudad.Nombre + "," + ciudad.Pais + "&units=metric"); //En una futura iteracón deberá inyectarse en el cliente

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Obteniendo Reporte");
                var report = await response.Content.ReadFromJsonAsync<ReportApi>();
                var reportBd = _mapper.Map<WeatherReportBd>(report);
                reportBd.CityId = ciudad.Id;

                await base.Add(reportBd);

                Expression<Func<WeatherReportBd, bool>> expression = c => c.CityId == ciudad.Id;
                Expression<Func<WeatherReportBd, object>>[] includeProperties = new Expression<Func<WeatherReportBd, object>>[]
            {
              o => o.CityBd
            };
                var reports = GetAll(expression, includeProperties).OrderByDescending(x => x.Validez).Take(historial ? MAXREGISTERS : 1).ToList();

                return _mapper.Map<List<WeatherRequestCity>>(reports);
            }
            _logger.LogWarning("No hubo respuesta de la Api");
            return new List<WeatherRequestCity>();
        }
    }
}
