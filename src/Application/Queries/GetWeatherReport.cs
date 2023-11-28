using MediatR;
using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Dtos.Request;

namespace WeatherRequest.Application.Queries
{
    public class GetWeatherReport : IRequest<List<WeatherRequestCity>>
    {
        public GetWeatherRequest GetWeatherRequest { get; set; } = null!;   
    }
}
