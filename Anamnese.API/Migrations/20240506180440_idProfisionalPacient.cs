using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class idProfisionalPacient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Pacient",
                newName: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProfissionalId",
                table: "Pacient",
                newName: "CreatedBy");
        }
    }
}
