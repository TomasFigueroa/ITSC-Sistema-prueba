using INSTITUTO.Bdat.Data.Entity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace INSTITUTO.Bdat
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Carrerass> Carreras => Set<Carrerass>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
          
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrerass>(o =>
            {
                o.HasKey(b => b.IdCarrera);
                o.Property(b => b.Nombre);
                o.Property(b => b.FechaInicio);
                o.Property(b => b.FechaFin);

            });
        }
    }
    
}
