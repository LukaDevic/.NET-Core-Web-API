using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class AddMatchDetailsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MatchDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LegueTitle = table.Column<string>(maxLength: 50, nullable: false),
                    Matchday = table.Column<int>(nullable: false),
                    Group = table.Column<string>(maxLength: 1, nullable: false),
                    HomeTeam = table.Column<string>(maxLength: 50, nullable: false),
                    AwayTeam = table.Column<string>(maxLength: 50, nullable: false),
                    KickoffAt = table.Column<DateTimeOffset>(nullable: false),
                    Score = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchDetails", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MatchDetails");
        }
    }
}
