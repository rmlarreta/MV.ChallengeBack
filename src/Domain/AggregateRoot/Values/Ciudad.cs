namespace WeatherRequest.Domain.AggregateRoot.Values
{
    public class Ciudad
    {
        public Guid Id { get; set; }
        public string Pais { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public double IdDouble { get; set; }
    }
}
