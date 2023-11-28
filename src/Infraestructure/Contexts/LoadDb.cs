using WeatherRequest.Infraestructure.Entities.Api;
using WeatherRequest.Infraestructure.Entities.Bd;

namespace WeatherRequest.Infraestructure.Contexts
{
    public class LoadDb
    {
        public static async Task InsertCities(WrContext dbContext, List<CityJson.Root> cities)
        {
            if (dbContext.Database.CanConnect())
            {
                // Agregar registros si es necesario
                if (!dbContext.Cities.Any())
                {
                    List<CityBd> citiesDb = new();
                    foreach (var city in cities)
                    {
                        CityBd cityBd = new()
                        { 
                            Name = city.name,
                            Pais = city.country,
                            IdDouble = city.id,
                        };
                        citiesDb.Add(cityBd);
                    }
                    // Agregar registros a la tabla
                    await dbContext.Cities.AddRangeAsync(citiesDb);

                    // Guardar los cambios en la base de datos
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}
