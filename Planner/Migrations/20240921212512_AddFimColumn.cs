using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class AddFimColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlocoDuracao",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeMetasCriadas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeMetasNaoCumpridas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTarefasCriadas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeTarefasNaoExecutadas",
                table: "Relatorios",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeMetasCriadas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "QuantidadeMetasNaoCumpridas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "QuantidadeTarefasCriadas",
                table: "Relatorios");

            migrationBuilder.DropColumn(
                name: "QuantidadeTarefasNaoExecutadas",
                table: "Relatorios");

            migrationBuilder.AddColumn<int>(
                name: "BlocoDuracao",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);
        }
    }
}
