using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class relationspecilityCod : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional");

            migrationBuilder.RenameColumn(
                name: "SpecialityId",
                table: "Profissional",
                newName: "SpecialityCode");

            migrationBuilder.RenameIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional",
                newName: "IX_Profissional_SpecialityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Speciality_SpecialityCode",
                table: "Profissional",
                column: "SpecialityCode",
                principalTable: "Speciality",
                principalColumn: "SpecialityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityCode",
                table: "Profissional");

            migrationBuilder.RenameColumn(
                name: "SpecialityCode",
                table: "Profissional",
                newName: "SpecialityId");

            migrationBuilder.RenameIndex(
                name: "IX_Profissional_SpecialityCode",
                table: "Profissional",
                newName: "IX_Profissional_SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "SpecialityCode");
        }
    }
}
