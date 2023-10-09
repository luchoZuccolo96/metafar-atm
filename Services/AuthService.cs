using metafar_atm.Data;
using metafar_atm.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;

namespace MetafarATM.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository authRepository, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _configuration = configuration;
        }

        public string EsValido(string cardNumber, string pin)
        {
            var resultado = _authRepository.EsValido(cardNumber, pin);
            return resultado;
        }

        public string GenerarTokenJWT(string cardNumber)
        {
            var authEntry = _authRepository.GetCard(cardNumber);

            if (authEntry != null && !authEntry.Blocked)
            {
                // Configuración de la clave secreta para firmar el token
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                // Reclamaciones (datos sobre el usuario) incluidos en el token
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, cardNumber),
                    new Claim("CardNumber", cardNumber), // Claim personalizado
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                // Configuración del token
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: credentials
                );

                // Cadena JWT firmada
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                // Tarjeta bloqueada, no se emite un token
                return null;
            }
        }
    }
}
