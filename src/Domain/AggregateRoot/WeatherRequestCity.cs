using WeatherRequest.Domain.AggregateRoot.Values;
namespace WeatherRequest.Domain.AggregateRoot
{
    public class WeatherRequestCity 
    {
        public Ciudad? Ciudad { get; set; } 
        public double? Clima { get; set; } //Temperatura Real
        public double? Termica { get; set; } //Sensaciòn Tèrmica
        public DateTime Validez { get; set; } // Ùltima Actualización
    }
}
