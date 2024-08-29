using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Planner.Migrations
{
    /// <inheritdoc />
    public partial class Lembrete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_lembretes",
            //    table: "lembretes");

            //migrationBuilder.RenameTable(
            //    name: "lembretes",
            //    newName: "Lembretes");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHora",
                table: "Lembretes",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "RecorrenteSemanal",
                table: "Lembretes",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "titulo",
                table: "Lembretes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lembretes",
                table: "Lembretes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Lembretes",
            //    table: "Lembretes");

            migrationBuilder.DropColumn(
                name: "DataHora",
                table: "Lembretes");

            migrationBuilder.DropColumn(
                name: "RecorrenteSemanal",
                table: "Lembretes");

            migrationBuilder.DropColumn(
                name: "titulo",
                table: "Lembretes");

            //migrationBuilder.RenameTable(
            //    name: "Lembretes",
            //    newName: "lembretes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lembretes",
                table: "lembretes",
                column: "Id");
        }
    }
}
