using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace metafar_atm.Models
{
    [Table("auth")]
    public class Auth
    {
        [Key]
        [Column(TypeName = "varchar(16)")]
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public int Attempts { get; set; }
        public bool Blocked { get; set; }
    }
}
