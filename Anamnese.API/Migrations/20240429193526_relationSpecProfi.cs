using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class relationSpecProfi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Speciality_Profissional_ProfissionalId",
                table: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Speciality_ProfissionalId",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "SpecialtyId",
                table: "Profissional");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Profissional",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional",
                column: "SpecialityId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "SpecialityId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Profissional");

            migrationBuilder.AddColumn<string>(
                name: "SpecialtyId",
                table: "Profissional",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Speciality_ProfissionalId",
                table: "Speciality",
                column: "ProfissionalId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Speciality_Profissional_ProfissionalId",
                table: "Speciality",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
