using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class AjustReferral : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referral_Pacient_PacientId",
                table: "Referral");

            migrationBuilder.DropColumn(
                name: "MedicalSpeciality",
                table: "Referral");

            migrationBuilder.DropColumn(
                name: "ReferralDate",
                table: "Referral");

            migrationBuilder.RenameColumn(
                name: "PacientName",
                table: "Referral",
                newName: "Speciality");

            migrationBuilder.RenameColumn(
                name: "PacientId",
                table: "Referral",
                newName: "ProfissionalId");

            migrationBuilder.RenameIndex(
                name: "IX_Referral_PacientId",
                table: "Referral",
                newName: "IX_Referral_ProfissionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Referral_Profissional_ProfissionalId",
                table: "Referral",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Referral_Profissional_ProfissionalId",
                table: "Referral");

            migrationBuilder.RenameColumn(
                name: "Speciality",
                table: "Referral",
                newName: "PacientName");

            migrationBuilder.RenameColumn(
                name: "ProfissionalId",
                table: "Referral",
                newName: "PacientId");

            migrationBuilder.RenameIndex(
                name: "IX_Referral_ProfissionalId",
                table: "Referral",
                newName: "IX_Referral_PacientId");

            migrationBuilder.AddColumn<string>(
                name: "MedicalSpeciality",
                table: "Referral",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<DateTime>(
                name: "ReferralDate",
                table: "Referral",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Referral_Pacient_PacientId",
                table: "Referral",
                column: "PacientId",
                principalTable: "Pacient",
                principalColumn: "PacientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
