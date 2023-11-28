using Microsoft.AspNetCore.Mvc;

namespace WeatherRequest.Controllers
{
    /// <summary>
    /// Controlador Base
    /// </summary>
    [ApiController]
    [Route("api/v1/[Controller]/[Action]")]    
    public abstract class CommonController : ControllerBase
    {
    }
}
