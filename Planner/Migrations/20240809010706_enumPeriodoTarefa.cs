using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class enumPeriodoTarefa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "Atividades");

            migrationBuilder.AddColumn<int>(
                name: "BlocoDuracao",
                table: "Atividades",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlocoDuracao",
                table: "Atividades");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Duracao",
                table: "Atividades",
                type: "TEXT",
                nullable: true);
        }
    }
}
