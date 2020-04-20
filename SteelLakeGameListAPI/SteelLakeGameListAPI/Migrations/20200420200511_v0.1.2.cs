using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SteelLakeGameListAPI.Migrations
{
    public partial class v012 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Mods",
                table: "Mods",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Expansions",
                table: "Expansions",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mods_Mods",
                table: "Mods",
                column: "Mods");

            migrationBuilder.CreateIndex(
                name: "IX_Expansions_Expansions",
                table: "Expansions",
                column: "Expansions");

            migrationBuilder.AddForeignKey(
                name: "FK_Expansions_Games_Expansions",
                table: "Expansions",
                column: "Expansions",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Mods_Games_Mods",
                table: "Mods",
                column: "Mods",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expansions_Games_Expansions",
                table: "Expansions");

            migrationBuilder.DropForeignKey(
                name: "FK_Mods_Games_Mods",
                table: "Mods");

            migrationBuilder.DropIndex(
                name: "IX_Mods_Mods",
                table: "Mods");

            migrationBuilder.DropIndex(
                name: "IX_Expansions_Expansions",
                table: "Expansions");

            migrationBuilder.DropColumn(
                name: "Mods",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "Expansions",
                table: "Expansions");
        }
    }
}
