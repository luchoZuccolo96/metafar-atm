using metafar_atm.Data;
using metafar_atm.Models;

namespace metafar_atm.Repositories
{
    public class TarjetaRepository : ITarjetaRepository
    {
        private readonly DBContext _dbContext;

        public TarjetaRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Tarjeta? ObtenerTarjetaPorNumero(string cardNumber)
        {
            return _dbContext.Tarjeta.FirstOrDefault(t => t.CardNumber == cardNumber);
        }

        public void ActualizarSaldo(string cardNumber, decimal monto)
        {
            var tarjeta = _dbContext.Tarjeta.FirstOrDefault(t => t.CardNumber == cardNumber);

            if (tarjeta != null)
            {
                tarjeta.SaldoActual -= monto;
                _dbContext.SaveChanges();
            }
        }

        public void AgregarOperacionHistorial(Historial historial)
        {
            _dbContext.Historial.Add(historial);
            _dbContext.SaveChanges();
        }

        public List<Historial> ObtenerHistorialOperacionesPaginado(string cardNumber, int pagina, int tamanoPagina)
        {
            var query = _dbContext.Historial
                .Where(h => h.CardNumber == cardNumber)
                .OrderByDescending(h => h.Fecha)
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            return query;
        }
    }

}
