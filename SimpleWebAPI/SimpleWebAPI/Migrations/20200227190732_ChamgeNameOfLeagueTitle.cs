using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class ChamgeNameOfLeagueTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LegueTitle",
                table: "MatchDetails");

            migrationBuilder.AddColumn<string>(
                name: "LeagueTitle",
                table: "MatchDetails",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeagueTitle",
                table: "MatchDetails");

            migrationBuilder.AddColumn<string>(
                name: "LegueTitle",
                table: "MatchDetails",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
