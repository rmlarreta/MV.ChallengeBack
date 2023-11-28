using Microsoft.Extensions.Caching.Memory;
using WeatherRequest.Infraestructure.Interfaces;

namespace WeatherRequest.Infraestructure.Services
{
    internal class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T GetOrSet<T>(string key, Func<T> getItemCallback, TimeSpan expirationTime)
        {
            if (!_memoryCache.TryGetValue(key, out T result))
            {
                // Los datos no están en caché, llama a la función para obtenerlos
                result = getItemCallback();

                // Almacena en caché con la expiración proporcionada
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expirationTime,
                };

                _memoryCache.Set(key, result, cacheEntryOptions);
            }

            return result;
        }
    }
}
