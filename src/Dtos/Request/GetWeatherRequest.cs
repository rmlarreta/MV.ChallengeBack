using WeatherRequest.Domain.AggregateRoot.Values;

namespace WeatherRequest.Dtos.Request
{
    public class GetWeatherRequest
    {
        public Ciudad Ciudad { get; set; } = null!;
        public bool Historial { get; set; }
    }
}
