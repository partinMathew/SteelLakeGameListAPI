using Microsoft.EntityFrameworkCore.Migrations;

namespace SteelLakeGameListAPI.Migrations
{
    public partial class Expansions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GameLength",
                table: "Expansions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaxNumberOfPlayers",
                table: "Expansions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinNumberOfPlayers",
                table: "Expansions",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RecommendedNumberOfPlayers",
                table: "Expansions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GameLength",
                table: "Expansions");

            migrationBuilder.DropColumn(
                name: "MaxNumberOfPlayers",
                table: "Expansions");

            migrationBuilder.DropColumn(
                name: "MinNumberOfPlayers",
                table: "Expansions");

            migrationBuilder.DropColumn(
                name: "RecommendedNumberOfPlayers",
                table: "Expansions");
        }
    }
}
