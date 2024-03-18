using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class relationPacientReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Pacient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Pacient",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
