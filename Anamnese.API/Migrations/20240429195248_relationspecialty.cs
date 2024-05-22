using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Anamnese.API.Migrations
{
    /// <inheritdoc />
    public partial class relationspecialty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "SpecialityId",
                table: "Speciality");

            migrationBuilder.DropColumn(
                name: "ProfissionalId",
                table: "Speciality");

            migrationBuilder.AddColumn<string>(
                name: "SpecialityCode",
                table: "Speciality",
                type: "varchar(255)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialityId",
                table: "Profissional",
                type: "varchar(255)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality",
                column: "SpecialityCode");

            migrationBuilder.CreateIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional",
                column: "SpecialityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional",
                column: "SpecialityId",
                principalTable: "Speciality",
                principalColumn: "SpecialityCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Profissional_Speciality_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality");

            migrationBuilder.DropIndex(
                name: "IX_Profissional_SpecialityId",
                table: "Profissional");

            migrationBuilder.DropColumn(
                name: "SpecialityCode",
                table: "Speciality");

            migrationBuilder.AddColumn<int>(
                name: "SpecialityId",
                table: "Speciality",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ProfissionalId",
                table: "Speciality",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SpecialityId",
                table: "Profissional",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "varchar(255)",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Speciality",
                table: "Speciality",
                column: "SpecialityId");

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
    }
}
