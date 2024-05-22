using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class withouSpecial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityCode",
                table: "Profissional");

            migrationBuilder.DropTable(
                name: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_SpecialityCode",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "SpecialityCode",
                table: "Profissional");

            migrationBuilder.AddColumn<string>(
                name: "Speciality",
                table: "Profissional",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Speciality",
                table: "Profissional");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityCode",
                table: "Profissional",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Speciality",
                columns: table => new
                {
                    SpecialityCode = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpecialityName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speciality", x => x.SpecialityCode);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_SpecialityCode",
                table: "Profissional",
                column: "SpecialityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Speciality_SpecialityCode",
                table: "Profissional",
                column: "SpecialityCode",
                principalTable: "Speciality",
                principalColumn: "SpecialityCode");
        }
    }
}
