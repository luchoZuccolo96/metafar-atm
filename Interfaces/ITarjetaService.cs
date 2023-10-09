using metafar_atm.Models;

namespace metafar_atm.Interfaces
{
    public interface ITarjetaService
    {
        public SaldoInfoDto? ObtenerSaldoInfo(string cardNumber);
        public OperacionRetiroDto RealizarRetiro(string cardNumber, decimal monto);
        public List<HistorialOperacionDto> ObtenerHistorialOperaciones(string cardNumber, int pagina, int tamanoPagina);
    }
}
