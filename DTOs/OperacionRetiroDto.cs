public class OperacionRetiroDto
{
    public bool Estado { get; set; }
    public decimal Retiro { get; set;}
    public decimal SaldoActual { get; set; }
    public DateTime Fecha { get; set; }
    public string Mensaje { get; set; }
}