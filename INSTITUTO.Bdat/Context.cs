using INSTITUTO.Bdat.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace INSTITUTO.Bdat
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Carrera> Carreras => Set<Carrera>();
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }
    }
}
