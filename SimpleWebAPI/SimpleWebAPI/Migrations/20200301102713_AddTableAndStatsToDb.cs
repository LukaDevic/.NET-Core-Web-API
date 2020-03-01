using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class AddTableAndStatsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeagueTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeagueTitle = table.Column<string>(nullable: true),
                    Matchday = table.Column<int>(nullable: false),
                    Group = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeagueTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeamStatisticses",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Rank = table.Column<int>(nullable: false),
                    Team = table.Column<string>(nullable: true),
                    PlayedGames = table.Column<int>(nullable: false),
                    Points = table.Column<int>(nullable: false),
                    Goals = table.Column<int>(nullable: false),
                    GoalsAgainst = table.Column<int>(nullable: false),
                    GoalDifference = table.Column<int>(nullable: false),
                    Win = table.Column<int>(nullable: false),
                    Lose = table.Column<int>(nullable: false),
                    Draw = table.Column<int>(nullable: false),
                    LeagueTableId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamStatisticses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeamStatisticses_LeagueTables_LeagueTableId",
                        column: x => x.LeagueTableId,
                        principalTable: "LeagueTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TeamStatisticses_LeagueTableId",
                table: "TeamStatisticses",
                column: "LeagueTableId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TeamStatisticses");

            migrationBuilder.DropTable(
                name: "LeagueTables");
        }
    }
}
