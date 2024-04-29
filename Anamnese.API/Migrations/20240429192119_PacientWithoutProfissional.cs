using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class PacientWithoutProfissional : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pacient_Profissional_ProfissionalId",
                table: "Pacient");

            migrationBuilder.DropIndex(
                name: "IX_Pacient_ProfissionalId",
                table: "Pacient");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "Pacient");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "Pacient",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pacient_ProfissionalId",
                table: "Pacient",
                column: "ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pacient_Profissional_ProfissionalId",
                table: "Pacient",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
