using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherRequest.Application.Queries;
using WeatherRequest.Domain.AggregateRoot.Values;

namespace WeatherRequest.Controllers
{
    public class CitiesController : CommonController
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly IMediator _mediator;

        public CitiesController(ILogger<CitiesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        /// <summary>
        /// Devuelve todas las ciudades soportadas por el sitema con sus datos de identificaciòn
        /// </summary>
        /// <returns>Listado de Ciudades</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<Ciudad>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetCities());
            if (result != null)
            {
                _logger.LogInformation("Devolviendo {registers} registros", result.Count);
                return Ok(result);
            }
            return NoContent();
        }

    }
}
