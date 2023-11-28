using WeatherRequest.Domain.AggregateRoot.Values;

namespace WeatherRequest.Domain.Interfaces
{
    public interface ICitiesService
    {
        Task<List<Ciudad>> GetAllCities();
    }
}
