using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class MigraçãoTeste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Relatorios",
                table: "Relatorios");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Relatorios",
                newName: "TurnosMaisProdutivos");

            migrationBuilder.AlterColumn<int>(
                name: "TurnosMaisProdutivos",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CategoriaMetaMaisRealizada",
                table: "Relatorios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CategoriaTarefaMaisRealizada",
                table: "Relatorios",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MesesMaisProdutivos",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Periodo",
                table: "Relatorios",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "PorcentagemMetasCumpridas",
                table: "Relatorios",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "PorcentagemTarefasExecutadas",
                table: "Relatorios",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeMetasCumpridas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTarefasExecutadas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SemanasMaisProdutivas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

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
                    Inicio = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    Fim = table.Column<TimeSpan>(type: "TEXT", nullable: false),
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CategoriaMetaMaisRealizada",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "CategoriaTarefaMaisRealizada",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "MesesMaisProdutivos",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "Periodo",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PorcentagemMetasCumpridas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "PorcentagemTarefasExecutadas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "QuantidadeMetasCumpridas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "QuantidadeTarefasExecutadas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "SemanasMaisProdutivas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "HorarioId",
                table: "Atividades");

            migrationBuilder.RenameColumn(
                name: "TurnosMaisProdutivos",
                table: "Relatorios",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Relatorios",
                table: "Relatorios",
                column: "Id");
        }
    }
}
