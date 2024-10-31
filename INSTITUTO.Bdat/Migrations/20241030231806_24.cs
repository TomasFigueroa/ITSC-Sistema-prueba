using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class _24 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notas_LIbros_LibrosId_Libro",
                table: "notas");

            migrationBuilder.RenameColumn(
                name: "LibrosId_Libro",
                table: "notas",
                newName: "LIbrosId_Libro");

            migrationBuilder.RenameIndex(
                name: "IX_notas_LibrosId_Libro",
                table: "notas",
                newName: "IX_notas_LIbrosId_Libro");

            migrationBuilder.AlterColumn<int>(
                name: "LIbrosId_Libro",
                table: "notas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_notas_LIbros_LIbrosId_Libro",
                table: "notas",
                column: "LIbrosId_Libro",
                principalTable: "LIbros",
                principalColumn: "Id_Libro");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notas_LIbros_LIbrosId_Libro",
                table: "notas");

            migrationBuilder.RenameColumn(
                name: "LIbrosId_Libro",
                table: "notas",
                newName: "LibrosId_Libro");

            migrationBuilder.RenameIndex(
                name: "IX_notas_LIbrosId_Libro",
                table: "notas",
                newName: "IX_notas_LibrosId_Libro");

            migrationBuilder.AlterColumn<int>(
                name: "LibrosId_Libro",
                table: "notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_notas_LIbros_LibrosId_Libro",
                table: "notas",
                column: "LibrosId_Libro",
                principalTable: "LIbros",
                principalColumn: "Id_Libro",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
