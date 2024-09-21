using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunasInicioFimTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Atividades_Horario_HorarioId",
                table: "Atividades");

            migrationBuilder.DropTable(
                name: "Horario");

            migrationBuilder.DropIndex(
                name: "IX_Atividades_HorarioId",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Atividades");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Fim",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Inicio",
                table: "Atividades",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Turnos",
                table: "Atividades",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fim",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Inicio",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "Turnos",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "HorarioId",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Horario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fim = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Inicio = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Turnos = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Horario", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atividades_HorarioId",
                table: "Atividades",
                column: "HorarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Atividades_Horario_HorarioId",
                table: "Atividades",
                column: "HorarioId",
                principalTable: "Horario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
