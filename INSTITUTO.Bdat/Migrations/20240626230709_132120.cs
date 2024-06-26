using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class _132120 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "alumnos",
                columns: table => new
                {
                    IdAlumno = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DNI_Alum = table.Column<int>(type: "int", nullable: false),
                    Cuil = table.Column<int>(type: "int", nullable: false),
                    Fecha_Nac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tbase = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nacionalidad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alumnos", x => x.IdAlumno);
                });

            migrationBuilder.CreateTable(
                name: "Carreras",
                columns: table => new
                {
                    IdCarrera = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carreras", x => x.IdCarrera);
                });

            migrationBuilder.CreateTable(
                name: "Ciclos",
                columns: table => new
                {
                    IdCiclo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciclos", x => x.IdCiclo);
                });

            migrationBuilder.CreateTable(
                name: "LIbros",
                columns: table => new
                {
                    Id_Libro = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre_Lib = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LIbros", x => x.Id_Libro);
                });

            migrationBuilder.CreateTable(
                name: "Materia",
                columns: table => new
                {
                    IdMateria = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DivisionesIdDivision = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materia", x => x.IdMateria);
                });

            migrationBuilder.CreateTable(
                name: "profesors",
                columns: table => new
                {
                    IdProfesor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Apellido_Prof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nombre_Prof = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dni = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_profesors", x => x.IdProfesor);
                });

            migrationBuilder.CreateTable(
                name: "TipoEvaluacions",
                columns: table => new
                {
                    IdTipoEva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEva = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoEvaluacions", x => x.IdTipoEva);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                columns: table => new
                {
                    IdDivision = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreDiv = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NombreCarrera = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarrerassIdCarrera = table.Column<int>(type: "int", nullable: false),
                    MateriasIdMateria = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.IdDivision);
                    table.ForeignKey(
                        name: "FK_Division_Carreras_CarrerassIdCarrera",
                        column: x => x.CarrerassIdCarrera,
                        principalTable: "Carreras",
                        principalColumn: "IdCarrera",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Division_Materia_MateriasIdMateria",
                        column: x => x.MateriasIdMateria,
                        principalTable: "Materia",
                        principalColumn: "IdMateria",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionCiclos",
                columns: table => new
                {
                    IdDivCic = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CicloIdCiclo = table.Column<int>(type: "int", nullable: false),
                    DivisionesIdDivision = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionCiclos", x => x.IdDivCic);
                    table.ForeignKey(
                        name: "FK_DivisionCiclos_Ciclos_CicloIdCiclo",
                        column: x => x.CicloIdCiclo,
                        principalTable: "Ciclos",
                        principalColumn: "IdCiclo",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivisionCiclos_Division_DivisionesIdDivision",
                        column: x => x.DivisionesIdDivision,
                        principalTable: "Division",
                        principalColumn: "IdDivision",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivisionCicloMaterias",
                columns: table => new
                {
                    IdDivCicMat = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionCicloIdDivCic = table.Column<int>(type: "int", nullable: false),
                    ProfesorIdProfesor = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivisionCicloMaterias", x => x.IdDivCicMat);
                    table.ForeignKey(
                        name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic",
                        column: x => x.DivisionCicloIdDivCic,
                        principalTable: "DivisionCiclos",
                        principalColumn: "IdDivCic",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivisionCicloMaterias_profesors_ProfesorIdProfesor",
                        column: x => x.ProfesorIdProfesor,
                        principalTable: "profesors",
                        principalColumn: "IdProfesor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DivsionCiclosMateriaAlumnos",
                columns: table => new
                {
                    IdDivCicMatAlum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DivisionCicloMateriaIdDivCicMat = table.Column<int>(type: "int", nullable: false),
                    AlumnosIdAlumno = table.Column<int>(type: "int", nullable: false),
                    LibrosId_Libro = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DivsionCiclosMateriaAlumnos", x => x.IdDivCicMatAlum);
                    table.ForeignKey(
                        name: "FK_DivsionCiclosMateriaAlumnos_DivisionCicloMaterias_DivisionCicloMateriaIdDivCicMat",
                        column: x => x.DivisionCicloMateriaIdDivCicMat,
                        principalTable: "DivisionCicloMaterias",
                        principalColumn: "IdDivCicMat",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivsionCiclosMateriaAlumnos_LIbros_LibrosId_Libro",
                        column: x => x.LibrosId_Libro,
                        principalTable: "LIbros",
                        principalColumn: "Id_Libro",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DivsionCiclosMateriaAlumnos_alumnos_AlumnosIdAlumno",
                        column: x => x.AlumnosIdAlumno,
                        principalTable: "alumnos",
                        principalColumn: "IdAlumno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "notas",
                columns: table => new
                {
                    IdNotas = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nota = table.Column<int>(type: "int", nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TipoEvaluacionIdTipoEva = table.Column<int>(type: "int", nullable: false),
                    DivsionCiclosMateriaAlumnosIdDivCicMatAlum = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notas", x => x.IdNotas);
                    table.ForeignKey(
                        name: "FK_notas_DivsionCiclosMateriaAlumnos_DivsionCiclosMateriaAlumnosIdDivCicMatAlum",
                        column: x => x.DivsionCiclosMateriaAlumnosIdDivCicMatAlum,
                        principalTable: "DivsionCiclosMateriaAlumnos",
                        principalColumn: "IdDivCicMatAlum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_notas_TipoEvaluacions_TipoEvaluacionIdTipoEva",
                        column: x => x.TipoEvaluacionIdTipoEva,
                        principalTable: "TipoEvaluacions",
                        principalColumn: "IdTipoEva",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Division_CarrerassIdCarrera",
                table: "Division",
                column: "CarrerassIdCarrera");

            migrationBuilder.CreateIndex(
                name: "IX_Division_MateriasIdMateria",
                table: "Division",
                column: "MateriasIdMateria");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCicloMaterias_DivisionCicloIdDivCic",
                table: "DivisionCicloMaterias",
                column: "DivisionCicloIdDivCic");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCicloMaterias_ProfesorIdProfesor",
                table: "DivisionCicloMaterias",
                column: "ProfesorIdProfesor");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCiclos_CicloIdCiclo",
                table: "DivisionCiclos",
                column: "CicloIdCiclo");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCiclos_DivisionesIdDivision",
                table: "DivisionCiclos",
                column: "DivisionesIdDivision");

            migrationBuilder.CreateIndex(
                name: "IX_DivsionCiclosMateriaAlumnos_AlumnosIdAlumno",
                table: "DivsionCiclosMateriaAlumnos",
                column: "AlumnosIdAlumno");

            migrationBuilder.CreateIndex(
                name: "IX_DivsionCiclosMateriaAlumnos_DivisionCicloMateriaIdDivCicMat",
                table: "DivsionCiclosMateriaAlumnos",
                column: "DivisionCicloMateriaIdDivCicMat");

            migrationBuilder.CreateIndex(
                name: "IX_DivsionCiclosMateriaAlumnos_LibrosId_Libro",
                table: "DivsionCiclosMateriaAlumnos",
                column: "LibrosId_Libro");

            migrationBuilder.CreateIndex(
                name: "IX_notas_DivsionCiclosMateriaAlumnosIdDivCicMatAlum",
                table: "notas",
                column: "DivsionCiclosMateriaAlumnosIdDivCicMatAlum");

            migrationBuilder.CreateIndex(
                name: "IX_notas_TipoEvaluacionIdTipoEva",
                table: "notas",
                column: "TipoEvaluacionIdTipoEva");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notas");

            migrationBuilder.DropTable(
                name: "DivsionCiclosMateriaAlumnos");

            migrationBuilder.DropTable(
                name: "TipoEvaluacions");

            migrationBuilder.DropTable(
                name: "DivisionCicloMaterias");

            migrationBuilder.DropTable(
                name: "LIbros");

            migrationBuilder.DropTable(
                name: "alumnos");

            migrationBuilder.DropTable(
                name: "DivisionCiclos");

            migrationBuilder.DropTable(
                name: "profesors");

            migrationBuilder.DropTable(
                name: "Ciclos");

            migrationBuilder.DropTable(
                name: "Division");

            migrationBuilder.DropTable(
                name: "Carreras");

            migrationBuilder.DropTable(
                name: "Materia");
        }
    }
}
