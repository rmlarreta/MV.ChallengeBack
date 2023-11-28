using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Domain.AggregateRoot.Values;

namespace WeatherRequest.Domain.Interfaces
{
    public interface IWeatherService
    {
        Task<List<WeatherRequestCity>> GetWeatherReport(Ciudad ciudad, bool historial,string appid); 
    }
}
