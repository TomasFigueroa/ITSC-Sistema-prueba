﻿// <auto-generated />
using System;
using INSTITUTO.Bdat;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20240626231149_1321200")]
    partial class _1321200
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.20")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Alumnos", b =>
                {
                    b.Property<int>("IdAlumno")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdAlumno"));

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Cuil")
                        .HasColumnType("int");

                    b.Property<int>("DNI_Alum")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Fecha_Nac")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nacionalidad")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tbase")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAlumno");

                    b.ToTable("alumnos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Carrerass", b =>
                {
                    b.Property<int>("IdCarrera")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCarrera"));

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdCarrera");

                    b.ToTable("Carreras");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Ciclo", b =>
                {
                    b.Property<int>("IdCiclo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdCiclo"));

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.HasKey("IdCiclo");

                    b.ToTable("Ciclos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCiclo", b =>
                {
                    b.Property<int>("IdDivCic")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDivCic"));

                    b.Property<int>("CicloIdCiclo")
                        .HasColumnType("int");

                    b.Property<int>("DivisionesIdDivision")
                        .HasColumnType("int");

                    b.HasKey("IdDivCic");

                    b.HasIndex("CicloIdCiclo");

                    b.HasIndex("DivisionesIdDivision");

                    b.ToTable("DivisionCiclos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCicloMateria", b =>
                {
                    b.Property<int>("IdDivCicMat")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDivCicMat"));

                    b.Property<int>("DivisionCicloIdDivCic")
                        .HasColumnType("int");

                    b.Property<int>("ProfesorIdProfesor")
                        .HasColumnType("int");

                    b.HasKey("IdDivCicMat");

                    b.HasIndex("DivisionCicloIdDivCic");

                    b.HasIndex("ProfesorIdProfesor");

                    b.ToTable("DivisionCicloMaterias");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Divisiones", b =>
                {
                    b.Property<int>("IdDivision")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDivision"));

                    b.Property<int>("CarrerassIdCarrera")
                        .HasColumnType("int");

                    b.Property<int>("MateriasIdMateria")
                        .HasColumnType("int");

                    b.Property<string>("NombreCarrera")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreDiv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDivision");

                    b.HasIndex("CarrerassIdCarrera");

                    b.HasIndex("MateriasIdMateria");

                    b.ToTable("Division");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivsionCiclosMateriaAlumnos", b =>
                {
                    b.Property<int>("IdDivCicMatAlum")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDivCicMatAlum"));

                    b.Property<int>("AlumnosIdAlumno")
                        .HasColumnType("int");

                    b.Property<int>("DivisionCicloMateriaIdDivCicMat")
                        .HasColumnType("int");

                    b.Property<int>("LibrosId_Libro")
                        .HasColumnType("int");

                    b.HasKey("IdDivCicMatAlum");

                    b.HasIndex("AlumnosIdAlumno");

                    b.HasIndex("DivisionCicloMateriaIdDivCicMat");

                    b.HasIndex("LibrosId_Libro");

                    b.ToTable("DivsionCiclosMateriaAlumnos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.LIbros", b =>
                {
                    b.Property<int>("Id_Libro")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id_Libro"));

                    b.Property<string>("Nombre_Lib")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id_Libro");

                    b.ToTable("LIbros");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Materias", b =>
                {
                    b.Property<int>("IdMateria")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdMateria"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdMateria");

                    b.ToTable("Materia");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Notas", b =>
                {
                    b.Property<int>("IdNotas")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdNotas"));

                    b.Property<int>("DivsionCiclosMateriaAlumnosIdDivCicMatAlum")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2");

                    b.Property<int>("Nota")
                        .HasColumnType("int");

                    b.Property<int>("TipoEvaluacionIdTipoEva")
                        .HasColumnType("int");

                    b.HasKey("IdNotas");

                    b.HasIndex("DivsionCiclosMateriaAlumnosIdDivCicMatAlum");

                    b.HasIndex("TipoEvaluacionIdTipoEva");

                    b.ToTable("notas");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Profesor", b =>
                {
                    b.Property<int>("IdProfesor")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProfesor"));

                    b.Property<string>("Apellido_Prof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Dni")
                        .HasColumnType("int");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre_Prof")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProfesor");

                    b.ToTable("profesors");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.TipoEvaluacion", b =>
                {
                    b.Property<int>("IdTipoEva")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdTipoEva"));

                    b.Property<string>("NombreEva")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdTipoEva");

                    b.ToTable("TipoEvaluacions");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCiclo", b =>
                {
                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Ciclo", "Ciclo")
                        .WithMany("divisionCiclos")
                        .HasForeignKey("CicloIdCiclo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Divisiones", "Divisiones")
                        .WithMany("divisionCiclos")
                        .HasForeignKey("DivisionesIdDivision")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ciclo");

                    b.Navigation("Divisiones");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCicloMateria", b =>
                {
                    b.HasOne("INSTITUTO.Bdat.Data.Entity.DivisionCiclo", "DivisionCiclo")
                        .WithMany("DivisionCicloMateria")
                        .HasForeignKey("DivisionCicloIdDivCic")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Profesor", "Profesor")
                        .WithMany()
                        .HasForeignKey("ProfesorIdProfesor")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DivisionCiclo");

                    b.Navigation("Profesor");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Divisiones", b =>
                {
                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Carrerass", "Carrerass")
                        .WithMany("divisions")
                        .HasForeignKey("CarrerassIdCarrera")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Materias", "materias")
                        .WithMany("Divisiones")
                        .HasForeignKey("MateriasIdMateria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrerass");

                    b.Navigation("materias");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivsionCiclosMateriaAlumnos", b =>
                {
                    b.HasOne("INSTITUTO.Bdat.Data.Entity.Alumnos", "Alumnos")
                        .WithMany("DivsionCiclosMateriaAlumnos")
                        .HasForeignKey("AlumnosIdAlumno")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.DivisionCicloMateria", "DivisionCicloMateria")
                        .WithMany("DivsionCiclosMateriaAlumnos")
                        .HasForeignKey("DivisionCicloMateriaIdDivCicMat")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.LIbros", "LIbros")
                        .WithMany("DivsionCiclosMateriaAlumnos")
                        .HasForeignKey("LibrosId_Libro")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alumnos");

                    b.Navigation("DivisionCicloMateria");

                    b.Navigation("LIbros");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Notas", b =>
                {
                    b.HasOne("INSTITUTO.Bdat.Data.Entity.DivsionCiclosMateriaAlumnos", "DivsionCiclosMateriaAlumnos")
                        .WithMany("Notas")
                        .HasForeignKey("DivsionCiclosMateriaAlumnosIdDivCicMatAlum")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("INSTITUTO.Bdat.Data.Entity.TipoEvaluacion", "TipoEvaluacion")
                        .WithMany("Notas")
                        .HasForeignKey("TipoEvaluacionIdTipoEva")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DivsionCiclosMateriaAlumnos");

                    b.Navigation("TipoEvaluacion");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Alumnos", b =>
                {
                    b.Navigation("DivsionCiclosMateriaAlumnos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Carrerass", b =>
                {
                    b.Navigation("divisions");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Ciclo", b =>
                {
                    b.Navigation("divisionCiclos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCiclo", b =>
                {
                    b.Navigation("DivisionCicloMateria");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivisionCicloMateria", b =>
                {
                    b.Navigation("DivsionCiclosMateriaAlumnos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Divisiones", b =>
                {
                    b.Navigation("divisionCiclos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.DivsionCiclosMateriaAlumnos", b =>
                {
                    b.Navigation("Notas");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.LIbros", b =>
                {
                    b.Navigation("DivsionCiclosMateriaAlumnos");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.Materias", b =>
                {
                    b.Navigation("Divisiones");
                });

            modelBuilder.Entity("INSTITUTO.Bdat.Data.Entity.TipoEvaluacion", b =>
                {
                    b.Navigation("Notas");
                });
#pragma warning restore 612, 618
        }
    }
}