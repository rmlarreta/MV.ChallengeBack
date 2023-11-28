using MediatR;
using WeatherRequest.Domain.AggregateRoot.Values;

namespace WeatherRequest.Application.Queries
{
    public class GetCities : IRequest<List<Ciudad>> { }
}
