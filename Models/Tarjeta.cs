using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metafar_atm.Models
{
    [Table("saldos")]
    public class Tarjeta
    {
        [Key]
        [Column(TypeName = "varchar(16)")]
        public string CardNumber { get; set; }
        public string NombreUsuario { get; set; }
        public string NumeroCuenta { get; set; }
        public decimal SaldoActual { get; set; }
        public DateTime UltimaExtraccion { get; set; }
    }
}
