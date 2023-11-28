namespace WeatherRequest.Infraestructure.Entities.Bd
{
    public class WeatherReportBd : Entity
    { 
        public Guid CityId { get; set; }
        public double? Temperatura { get; set; } //Temperatura Real
        public double? Termica { get; set; } //Sensaciòn Tèrmica
        public DateTime Validez { get; set; } // Ùltima Actualización

        public CityBd? CityBd { get; set; } //Entidad de navegación. Muy importante si seguimos codeFirst
    }
}
