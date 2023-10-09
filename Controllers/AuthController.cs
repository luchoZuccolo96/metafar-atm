using Microsoft.AspNetCore.Mvc;
using metafar_atm.Interfaces;

namespace MetafarATM.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login(string cardNumber, string pin)
        {
            var validationResult = _authService.EsValido(cardNumber, pin);

            if (validationResult == "A")
            {
                var token = _authService.GenerarTokenJWT(cardNumber);
                if (token != null)
                {
                    return Ok(new { token });
                }
            }
            else if (validationResult == "B")
            {
                return Unauthorized("La tarjeta está bloqueada. Comunícate con soporte.");
            }
            else
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return BadRequest("Error de sistema, vuelva a intentar.");
        }
    }
}
