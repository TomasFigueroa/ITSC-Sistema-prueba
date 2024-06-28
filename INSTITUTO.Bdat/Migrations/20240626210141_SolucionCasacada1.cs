using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class SolucionCasacada1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_profesors_ProfesorIdProfesor",
                table: "DivisionCicloMaterias");

            migrationBuilder.AddColumn<int>(
                name: "DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MateriasIdMateria1",
                table: "DivisionCicloMaterias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCicloMaterias_DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias",
                column: "DivisionCicloIdDivCic1");

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCicloMaterias_MateriasIdMateria1",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria1");

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic",
                table: "DivisionCicloMaterias",
                column: "DivisionCicloIdDivCic",
                principalTable: "DivisionCiclos",
                principalColumn: "IdDivCic",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias",
                column: "DivisionCicloIdDivCic1",
                principalTable: "DivisionCiclos",
                principalColumn: "IdDivCic");

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria",
                principalTable: "Materia",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria1",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria1",
                principalTable: "Materia",
                principalColumn: "IdMateria");

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_profesors_ProfesorIdProfesor",
                table: "DivisionCicloMaterias",
                column: "ProfesorIdProfesor",
                principalTable: "profesors",
                principalColumn: "IdProfesor",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria1",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_profesors_ProfesorIdProfesor",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropIndex(
                name: "IX_DivisionCicloMaterias_DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropIndex(
                name: "IX_DivisionCicloMaterias_MateriasIdMateria1",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropColumn(
                name: "DivisionCicloIdDivCic1",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropColumn(
                name: "MateriasIdMateria1",
                table: "DivisionCicloMaterias");

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_DivisionCiclos_DivisionCicloIdDivCic",
                table: "DivisionCicloMaterias",
                column: "DivisionCicloIdDivCic",
                principalTable: "DivisionCiclos",
                principalColumn: "IdDivCic",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria",
                principalTable: "Materia",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_profesors_ProfesorIdProfesor",
                table: "DivisionCicloMaterias",
                column: "ProfesorIdProfesor",
                principalTable: "profesors",
                principalColumn: "IdProfesor",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
