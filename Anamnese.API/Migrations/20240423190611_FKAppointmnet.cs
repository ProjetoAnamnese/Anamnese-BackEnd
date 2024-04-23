using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class FKAppointmnet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledAppointment_Pacient_PacientId",
                table: "ScheduledAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledAppointment_Profissional_ProfissionalId",
                table: "ScheduledAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledAppointment_PacientId",
                table: "ScheduledAppointment");

            migrationBuilder.DropColumn(
                name: "PacientId",
                table: "ScheduledAppointment");

            migrationBuilder.RenameColumn(
                name: "ProfissionalId",
                table: "ScheduledAppointment",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledAppointment_ProfissionalId",
                table: "ScheduledAppointment",
                newName: "IX_ScheduledAppointment_PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledAppointment_ProfessionalId",
                table: "ScheduledAppointment",
                column: "ProfessionalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledAppointment_Pacient_PatientId",
                table: "ScheduledAppointment",
                column: "PatientId",
                principalTable: "Pacient",
                principalColumn: "PacientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledAppointment_Profissional_ProfessionalId",
                table: "ScheduledAppointment",
                column: "ProfessionalId",
                principalTable: "Profissional",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledAppointment_Pacient_PatientId",
                table: "ScheduledAppointment");

            migrationBuilder.DropForeignKey(
                name: "FK_ScheduledAppointment_Profissional_ProfessionalId",
                table: "ScheduledAppointment");

            migrationBuilder.DropIndex(
                name: "IX_ScheduledAppointment_ProfessionalId",
                table: "ScheduledAppointment");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "ScheduledAppointment",
                newName: "ProfissionalId");

            migrationBuilder.RenameIndex(
                name: "IX_ScheduledAppointment_PatientId",
                table: "ScheduledAppointment",
                newName: "IX_ScheduledAppointment_ProfissionalId");

            migrationBuilder.AddColumn<int>(
                name: "PacientId",
                table: "ScheduledAppointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ScheduledAppointment_PacientId",
                table: "ScheduledAppointment",
                column: "PacientId");

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledAppointment_Pacient_PacientId",
                table: "ScheduledAppointment",
                column: "PacientId",
                principalTable: "Pacient",
                principalColumn: "PacientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ScheduledAppointment_Profissional_ProfissionalId",
                table: "ScheduledAppointment",
                column: "ProfissionalId",
                principalTable: "Profissional",
                principalColumn: "ProfissionalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
