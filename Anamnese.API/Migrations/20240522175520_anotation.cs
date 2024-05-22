using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class anotation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anotation",
                columns: table => new
                {
                    AnotationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PacientId = table.Column<int>(type: "int", nullable: false),
                    ProfissionalId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Comments = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anotation", x => x.AnotationId);
                    table.ForeignKey(
                        name: "FK_Anotation_Pacient_PacientId",
                        column: x => x.PacientId,
                        principalTable: "Pacient",
                        principalColumn: "PacientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Anotation_Profissional_ProfissionalId",
                        column: x => x.ProfissionalId,
                        principalTable: "Profissional",
                        principalColumn: "ProfissionalId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Anotation_PacientId",
                table: "Anotation",
                column: "PacientId");

            migrationBuilder.CreateIndex(
                name: "IX_Anotation_ProfissionalId",
                table: "Anotation",
                column: "ProfissionalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anotation");
        }
    }
}
