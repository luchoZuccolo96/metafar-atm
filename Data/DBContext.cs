using metafar_atm.Models;
using Microsoft.EntityFrameworkCore;

namespace metafar_atm.Data
{
    public class DBContext : DbContext
    {
        public DbSet<Auth> Auth { get; set; }
        public DbSet<Tarjeta> Tarjeta { get; internal set; }
        public DbSet<Historial> Historial { get; internal set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }
}
