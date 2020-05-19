using Microsoft.EntityFrameworkCore.Migrations;

namespace SteelLakeGameListAPI.Migrations
{
    public partial class UpdatedModSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameLength",
                table: "Mods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfPlayers",
                table: "Mods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinNumberOfPlayers",
                table: "Mods",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedNumberOfPlayers",
                table: "Mods",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameLength",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "MaxNumberOfPlayers",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "MinNumberOfPlayers",
                table: "Mods");

            migrationBuilder.DropColumn(
                name: "RecommendedNumberOfPlayers",
                table: "Mods");
        }
    }
}
