using Microsoft.EntityFrameworkCore;
using Modelo;
using Persistencia.ConfiModelo;
namespace Persistencia
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new EncabezadoConfig(modelBuilder.Entity<Encabezado>());
            new DetalleConfig(modelBuilder.Entity<Detalle>());
            new SexoConfig(modelBuilder.Entity<Sexo>());
        }
        public DbSet<Sexo>Sexo { get; set; }
        public DbSet<Encabezado>Encabezado { get; set; }
        public DbSet<Detalle> Detalle { get; set; }
        public DbSet<Estudiante> Estudiante { get; set; }
    }
}
