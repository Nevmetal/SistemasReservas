using Microsoft.EntityFrameworkCore;

namespace SistemaReservaCitas.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Cita> Citas { get; set; }
    }
}
