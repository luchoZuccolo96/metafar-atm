using Microsoft.AspNetCore.Mvc;
using metafar_atm.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace MetafarATM.Controllers
{
    [Route("api/tarjeta")]
    [ApiController]
    [Authorize]
    public class TarjetaController : ControllerBase
    {
        private readonly ITarjetaService _tarjetaService;

        public TarjetaController(ITarjetaService tarjetaService)
        {
            _tarjetaService = tarjetaService;
        }

        private bool ValidarTarjeta(string cardNumber)
        {
            string cardNumberStr = cardNumber.ToString();
            //revisar si la tarjeta le corresponde al usuario ingresado
            var userClaim = User.Claims.FirstOrDefault(c => c.Type == "CardNumber");
            return userClaim != null && userClaim.Value == cardNumberStr;
        }

        [HttpGet("saldo/{cardNumber}")]
        public IActionResult Saldo(string cardNumber)
        {
            if (!ValidarTarjeta(cardNumber))
            {
                return BadRequest("Tarjeta no encontrada.");
            }

            try
            {
                // Llama al servicio para obtener la información de saldo
                var saldoInfo = _tarjetaService.ObtenerSaldoInfo(cardNumber);
                if (saldoInfo == null)
                {
                    return BadRequest("Tarjeta no encontrada");
                }
                return Ok(saldoInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("retiro")]
        public IActionResult Retiro([FromBody] RetiroRequestDto request)
        {
            if (!ValidarTarjeta(request.CardNumber))
            {
                return BadRequest("Tarjeta no encontrada.");
            }
            var resultadoRetiro = _tarjetaService.RealizarRetiro(request.CardNumber, request.Monto);
            if (resultadoRetiro.Estado)
            {
                return Ok(resultadoRetiro);
            }
            else
            {
                return BadRequest(resultadoRetiro.Mensaje);
            }
        }

        [HttpGet("operaciones/{cardNumber}")]
        public IActionResult ObtenerHistorialOperaciones(string cardNumber, int pagina = 1, int tamanoPagina = 10)
        {
            if (!ValidarTarjeta(cardNumber))
            {
                return BadRequest("Tarjeta no encontrada.");
            }
            try
            {
                // Llama al servicio para obtener el historial de operaciones paginado
                var historial = _tarjetaService.ObtenerHistorialOperaciones(cardNumber, pagina, tamanoPagina);

                return Ok(historial);
            }
            catch (Exception ex)
            {
                // Maneja las excepciones según sea necesario
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

    }
}