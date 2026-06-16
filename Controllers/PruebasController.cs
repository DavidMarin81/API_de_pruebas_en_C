using API_de_pruebas.WSResponse;
using Microsoft.AspNetCore.Mvc;

namespace API_de_pruebas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PruebaController : ControllerBase
    {

        /// <summary>
        /// Comprueba que la API está funcionando.
        /// </summary>
        /// <remarks>
        /// Requiere enviar la cabecera X-Api-Key.
        /// </remarks>
        /// <response code="200">La API responde correctamente.</response>
        /// <response code="401">API Key no enviada o incorrecta.</response>
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return BadRequest(new WSResponse<object>
            {
                ErrorCode = 1,
                Message = "ApiKey incorrecta",
                Response = null
            });

            return Ok(new WSResponse<object>
            {
                ErrorCode = 0,
                Message = "Pong",
                Response = null
            });
        }
    }
}
