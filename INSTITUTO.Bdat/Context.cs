using INSTITUTO.Bdat.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Reflection.Emit;

namespace INSTITUTO.Bdat
{
    public class Context : DbContext
    {
        public Context() { }
        public Context(DbContextOptions<Context> options) : base(options) { }
        public DbSet<Carrerass> Carreras => Set<Carrerass>();
        public DbSet<Divisiones> Division => Set<Divisiones>();
        public DbSet<Materias> Materia => Set<Materias>();
        public DbSet<Ciclos> Ciclos => Set<Ciclos>();
        public DbSet<DivisionCicloMateria> DivisionCicloMaterias => Set<DivisionCicloMateria>();
        public DbSet<DivisionCiclo> DivisionCiclos => Set<DivisionCiclo>();
        public DbSet<DivsionCiclosMateriaAlumnos> DivsionCiclosMateriaAlumnos => Set<DivsionCiclosMateriaAlumnos>();
        public DbSet<Alumnos> alumnos => Set<Alumnos>();
        public DbSet<Profesor> profesors => Set<Profesor>();
        public DbSet<Notas> notas => Set<Notas>();
        public DbSet<TipoEvaluacion> TipoEvaluacions => Set<TipoEvaluacion>();
        public DbSet<LIbros> LIbros => Set<LIbros>();
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
            modelBuilder.Entity<Divisiones>(o =>
            {
                o.HasKey(b => b.IdDivision);
                o.Property(b => b.NombreDiv);
                

            });
            modelBuilder.Entity<Materias>(o =>
            {
                o.HasKey(b => b.IdMateria);
                o.Property(b => b.Nombre);

            });
            modelBuilder.Entity<Ciclos>(o =>
            {
                o.HasKey(b => b.IdCiclo);
                o.Property(b => b.Fecha);

            });
            modelBuilder.Entity<DivisionCiclo>(o =>
            {
                o.HasKey(b => b.IdDivCic);


            });
            modelBuilder.Entity<DivisionCicloMateria>(o =>
            {
                o.HasKey(b => b.IdDivCicMat);

            });
            modelBuilder.Entity<DivsionCiclosMateriaAlumnos>(o =>
            {
                o.HasKey(b => b.IdDivCicMatAlum);



            });
            modelBuilder.Entity<Alumnos>(o =>
            {
                o.HasKey(b => b.IdAlumno);
                o.Property(b => b.Nombre);
                o.Property(b => b.Apellido);
                o.Property(b => b.DNI_Alum);
                o.Property(b => b.Cuil);
                o.Property(b => b.Fecha_Nac);
                o.Property(b => b.Nacionalidad);
                o.Property(b => b.Estado);
                o.Property(b => b.Tbase);

            });
            modelBuilder.Entity<Profesor>(o =>
            {
                o.HasKey(b => b.IdProfesor);
                o.Property(b => b.Nombre_Prof);
                o.Property(b => b.Apellido_Prof);
                o.Property(b => b.Dni);
                o.Property(b => b.Estado);


            });
            modelBuilder.Entity<LIbros>(o =>
            {
                o.HasKey(b => b.Id_Libro);
                o.Property(b => b.Nombre_Lib);


            });
            modelBuilder.Entity<Notas>(o =>
            {
                o.HasKey(b => b.IdNotas);
                o.Property(b => b.Nota);
                o.Property(b => b.Fecha);


            });
            modelBuilder.Entity<TipoEvaluacion>(o =>
            {
                o.HasKey(b => b.IdTipoEva);
                o.Property(b => b.NombreEva);

            });
           
        }
    }

}
