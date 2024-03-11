using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "Birth",
                table: "Pacient",
                type: "date",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Pacient",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Report",
                columns: table => new
                {
                    ReportId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ReportDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MedicalHistory = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CurrentMedications = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CardiovascularIssues = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Diabetes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FamilyHistoryCardiovascularIssues = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FamilyHistoryDiabetes = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PhysicalActivity = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Smoker = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AlcoholConsumption = table.Column<int>(type: "int", nullable: false),
                    EmergencyContactName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    EmergencyContactPhone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observations = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PacientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Report", x => x.ReportId);
                    table.ForeignKey(
                        name: "FK_Report_Pacient_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacient",
                        principalColumn: "PacientId",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Report_PacientId",
                table: "Report",
                column: "PacientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Report");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Pacient");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Birth",
                table: "Pacient",
                type: "datetime(6)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }
    }
}
