namespace WeatherRequest.Infraestructure.Interfaces
{
    public interface ICacheService
    {
        T GetOrSet<T>(string key, Func<T> getItemCallback, TimeSpan expirationTime);
    }
}
