using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class ProfessionalAvailable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProfessionalAvailable",
                columns: table => new
                {
                    ProfessionalAvailableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProfessionalId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ProfessionalModelProfissionalId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProfessionalAvailable", x => x.ProfessionalAvailableId);
                    table.ForeignKey(
                        name: "FK_ProfessionalAvailable_Profissional_ProfessionalModelProfissi~",
                        column: x => x.ProfessionalModelProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "ProfissionalId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "DayOfWeekAvailable",
                columns: table => new
                {
                    DayOfWeekAvailableId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DayOfWeek = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfessionalAvailableId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayOfWeekAvailable", x => x.DayOfWeekAvailableId);
                    table.ForeignKey(
                        name: "FK_DayOfWeekAvailable_ProfessionalAvailable_ProfessionalAvailab~",
                        column: x => x.ProfessionalAvailableId,
                        principalTable: "ProfessionalAvailable",
                        principalColumn: "ProfessionalAvailableId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DayOfWeekAvailable_ProfessionalAvailableId",
                table: "DayOfWeekAvailable",
                column: "ProfessionalAvailableId");

            migrationBuilder.CreateIndex(
                name: "IX_ProfessionalAvailable_ProfessionalModelProfissionalId",
                table: "ProfessionalAvailable",
                column: "ProfessionalModelProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayOfWeekAvailable");

            migrationBuilder.DropTable(
                name: "ProfessionalAvailable");
        }
    }
}
