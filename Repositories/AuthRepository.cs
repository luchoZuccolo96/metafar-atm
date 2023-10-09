using metafar_atm.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Linq;
using metafar_atm.Models;

namespace metafar_atm.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DBContext _dbContext;
        private readonly IConfiguration _configuration;

        public AuthRepository(DBContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public string EsValido(string cardNumber, string pin)
        {
            var authEntry = _dbContext.Auth.FirstOrDefault(a => a.CardNumber == cardNumber);

            if (authEntry != null)
            {
                if (authEntry.Blocked)
                {
                    return "B";
                }

                if (authEntry.Pin == pin)
                {
                    if (authEntry.Attempts < 4)
                    {
                        authEntry.Attempts = 0;
                        _dbContext.SaveChanges();
                        // Credenciales válidas
                        return "A";
                    }
                    else
                    {
                        // Bloquear la tarjeta
                        authEntry.Blocked = true;
                        _dbContext.SaveChanges();
                        return "B"; // Tarjeta bloqueada después del cuarto intento fallido
                    }
                }
                else
                {
                    authEntry.Attempts++;
                    _dbContext.SaveChanges();
                    if (authEntry.Attempts >= 4)
                    {
                        // Bloquear la tarjeta después del cuarto intento fallido
                        authEntry.Blocked = true;
                        _dbContext.SaveChanges();
                        return "B";
                    }
                    else
                    {
                        // Credenciales inválidas
                        return "R";
                    }
                }
            }
            // Credenciales inválidas
            return "R";
        }

        public Auth GetCard(string cardNumber)
        {
            var authEntry = _dbContext.Auth.FirstOrDefault(a => a.CardNumber == cardNumber);
            return authEntry;
        }
    }
}

