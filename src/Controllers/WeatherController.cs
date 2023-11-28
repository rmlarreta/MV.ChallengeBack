using MediatR;
using Microsoft.AspNetCore.Mvc;
using WeatherRequest.Application.Queries;
using WeatherRequest.Domain.AggregateRoot;
using WeatherRequest.Dtos.Request;

namespace WeatherRequest.Controllers
{
    public class WeatherController : CommonController
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IMediator _mediator;

        public WeatherController(ILogger<WeatherController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }


        /// <summary>
        /// Devuelve el ùltimo dato meteorólogico de la ciudad solicitada.Devuelve el historial como opcional.
        /// </summary>
        /// <param name="city"></param>
        /// <returns>Listado de Datos Meteorológicos por ciudad</returns>
        [HttpPost]
        [ProducesResponseType(typeof(List<WeatherRequestCity>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromBody] GetWeatherRequest city)
        {
            var result = await _mediator.Send(new GetWeatherReport { GetWeatherRequest = city });
            if (result != null)
            {
                _logger.LogInformation("Devolviendo {registers} registros", result.Count);
                return Ok(result);
            }
            return NoContent();
        }
    }
}
