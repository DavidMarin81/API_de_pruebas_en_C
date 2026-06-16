using API_de_pruebas.WSResponse;
using Microsoft.AspNetCore.Mvc;
using API_de_pruebas.DTOs;

namespace API_de_pruebas.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NowController : ControllerBase
    {

        /// <summary>
        /// Obtiene la fecha actual del servidor.
        /// </summary>
        /// <remarks>
        /// Requiere enviar la cabecera X-Api-Key.
        /// </remarks>
        /// <response code="200">La API responde correctamente.</response>
        /// <response code="401">API Key no enviada o incorrecta.</response>
        [HttpGet("fecha")]
        public IActionResult obtenerFecha()
        {
            var fechaActual = DateTime.Now;

            return Ok(new WSResponse<string>
            {
                ErrorCode = 0,
                Message = "Fecha actual obtenida correctamente",
                Response = fechaActual.ToString("dd-MM-yyyy")
            });
        }

        /// <summary>
        /// Obtiene la hora actual del servidor.
        /// </summary>
        /// <remarks>
        /// Requiere enviar la cabecera X-Api-Key.
        /// </remarks>
        /// <response code="200">La API responde correctamente.</response>
        /// <response code="401">API Key no enviada o incorrecta.</response>
        [HttpGet("hora")]
        public IActionResult obtenerHora()
        {
            var fechaActual = DateTime.Now;

            return Ok(new WSResponse<string>
            {
                ErrorCode = 0,
                Message = "Fecha actual obtenida correctamente",
                Response = fechaActual.ToString("HH:mm:ss")
            });
        }

        /// <summary>
        /// Obtiene el precio con el descuento
        /// </summary>
        /// <param name="precio"></param>
        /// <returns></returns>
        [HttpGet("precioThomann")]
        public IActionResult obtenerPrecioThomann([FromQuery] decimal precio)
        {
            if (precio <= 0)
            {
                return BadRequest(new WSResponse<object>
                {
                    ErrorCode = -1,
                    Message = "El importe no puede ser menor o igual que 0",
                    Response= precio.ToString()
                });
            }

            precio *= 0.83m;

            precio = Math.Round(precio, 2);

            return Ok(new WSResponse<object>
            {
                ErrorCode = 0,
                Message = "Precio calculado correctamente",
                Response = precio.ToString()
            });
        }

        /// <summary>
        /// Calcula el precio Thomann aplicando un 17% de descuento.
        /// </summary>
        /// <param name="peticion">Datos enviados en el cuerpo de la petición.</param>
        /// <response code="200">Precio calculado correctamente.</response>
        /// <response code="400">El precio recibido no es válido.</response>
        [HttpPost("precioThomann")]
        public IActionResult obtenerPrecioThomanConDescuento([FromBody] CalcularPrecioRequest peticion)
        {
            if (peticion.Precio <= 0)
            {
                return BadRequest(new WSResponse<object>
                {
                    ErrorCode = -1,
                    Message = "El importe no puede ser menor o igual que 0",
                    Response = null
                });
            }

            decimal precioFinal = Math.Round(
                peticion.Precio * 0.83m,
                2,
                MidpointRounding.AwayFromZero);

            return Ok(new WSResponse<object>
            {
                ErrorCode = 0,
                Message = "Precio calculado correctamente",
                Response = new
                {
                    PrecioOriginal = peticion.Precio,
                    PrecioConDescuento = precioFinal
                }
            });
        }
    }
}
