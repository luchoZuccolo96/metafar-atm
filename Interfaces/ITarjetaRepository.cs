using metafar_atm.Models;

public interface ITarjetaRepository
{
    Tarjeta? ObtenerTarjetaPorNumero(string cardNumber);
    void ActualizarSaldo(string cardNumber, decimal monto);
    void AgregarOperacionHistorial(Historial historial);
    List<Historial> ObtenerHistorialOperacionesPaginado(string cardNumber, int pagina, int tamanoPagina);

}

