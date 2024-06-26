using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace INSTITUTO.Bdat.Migrations
{
    /// <inheritdoc />
    public partial class _1321200 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DivisionesIdDivision",
                table: "Materia");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DivisionesIdDivision",
                table: "Materia",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
