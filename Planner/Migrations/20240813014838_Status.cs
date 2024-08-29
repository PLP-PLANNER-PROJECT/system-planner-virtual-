using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class Status : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusAtividade",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "StatusMeta",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusTarefa",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusMeta",
                table: "Atividades");

            migrationBuilder.DropColumn(
                name: "StatusTarefa",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "StatusAtividade",
                table: "Atividades",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
