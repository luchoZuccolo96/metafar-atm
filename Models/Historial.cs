using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metafar_atm.Models
{
    [Table("historialOperaciones")]
    public class Historial
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(16)")]
        public string CardNumber { get; set; }
        public decimal MontoRetirado { get; set; }
        public decimal SaldoRestante { get; set; }
        [Column(TypeName = "date")]
        public DateTime Fecha { get; set; }
    }

}
