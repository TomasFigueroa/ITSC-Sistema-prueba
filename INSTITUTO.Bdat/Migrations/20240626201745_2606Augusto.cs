using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class _2606Augusto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MateriasIdMateria",
                table: "DivisionCicloMaterias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DivisionCicloMaterias_MateriasIdMateria",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria");

            migrationBuilder.AddForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias",
                column: "MateriasIdMateria",
                principalTable: "Materia",
                principalColumn: "IdMateria",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DivisionCicloMaterias_Materia_MateriasIdMateria",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropIndex(
                name: "IX_DivisionCicloMaterias_MateriasIdMateria",
                table: "DivisionCicloMaterias");

            migrationBuilder.DropColumn(
                name: "MateriasIdMateria",
                table: "DivisionCicloMaterias");
        }
    }
}
