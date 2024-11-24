using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class nuevo2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notas_LIbros_LIbrosId_Libro",
                table: "notas");

            migrationBuilder.AlterColumn<int>(
                name: "LIbrosId_Libro",
                table: "notas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LIbroId_Libro",
                table: "notas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_notas_LIbros_LIbrosId_Libro",
                table: "notas",
                column: "LIbrosId_Libro",
                principalTable: "LIbros",
                principalColumn: "Id_Libro",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_notas_LIbros_LIbrosId_Libro",
                table: "notas");

            migrationBuilder.DropColumn(
                name: "LIbroId_Libro",
                table: "notas");

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
    }
}
