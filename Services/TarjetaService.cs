using metafar_atm.Data;
using metafar_atm.Interfaces;
using metafar_atm.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace MetafarATM.Services
{
    public class TarjetaService : ITarjetaService
    {
        private readonly ITarjetaRepository _tarjetaRepository;

        public TarjetaService(ITarjetaRepository tarjetaRepository)
        {
            _tarjetaRepository = tarjetaRepository;
        }

        public SaldoInfoDto? ObtenerSaldoInfo(string cardNumber)
        {
            var tarjeta = _tarjetaRepository.ObtenerTarjetaPorNumero(cardNumber);

            if (tarjeta == null)
            {
                return null;
            }

            // Mapea los datos a un objeto anónimo
            var saldoInfo = new SaldoInfoDto
            {
                NombreUsuario = tarjeta.NombreUsuario,
                NumeroCuenta = tarjeta.NumeroCuenta,
                SaldoActual = tarjeta.SaldoActual,
                UltimaExtraccion = tarjeta.UltimaExtraccion
            };

            return saldoInfo;
        }
        public OperacionRetiroDto RealizarRetiro(string cardNumber, decimal monto)
        {
            var tarjeta = _tarjetaRepository.ObtenerTarjetaPorNumero(cardNumber);

            if (tarjeta == null)
            {
                return new OperacionRetiroDto
                {
                    Estado = false,
                    Mensaje = "Tarjeta no encontrada."
                };
            }

            if (tarjeta.SaldoActual < monto)
            {
                return new OperacionRetiroDto
                {
                    Estado = false,
                    Mensaje = "Saldo insuficiente."
                };
            }

            // Realiza el retiro y actualiza el saldo
            _tarjetaRepository.ActualizarSaldo(cardNumber, monto);

            // Guarda la operación en el historial de operaciones
            var nuevaOperacion = new Historial
            {
                CardNumber = cardNumber,
                MontoRetirado = monto,
                SaldoRestante = tarjeta.SaldoActual,
                Fecha = DateTime.UtcNow
            };

            _tarjetaRepository.AgregarOperacionHistorial(nuevaOperacion);

            return new OperacionRetiroDto
            {
                Estado = true,
                Retiro = monto,
                SaldoActual = tarjeta.SaldoActual,
                Fecha = DateTime.UtcNow,
                Mensaje = "Retiro exitoso."
            };
        }


        public List<HistorialOperacionDto> ObtenerHistorialOperaciones(string cardNumber, int pagina, int tamanoPagina)
        {
            var historialOperaciones = _tarjetaRepository.ObtenerHistorialOperacionesPaginado(cardNumber, pagina, tamanoPagina);

            // Mapear los resultados a DTOs
            var historialDto = historialOperaciones.Select(h => new HistorialOperacionDto
            {
                CardNumber = h.CardNumber,
                MontoRetirado = h.MontoRetirado,
                SaldoRestante = h.SaldoRestante,
                Fecha = h.Fecha
            }).ToList();

            return historialDto;
        }

    }
}




