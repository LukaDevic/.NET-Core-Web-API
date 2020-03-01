using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class ChangeKickofTypeToDAteTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "KickoffAt",
                table: "MatchDetails",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "KickoffAt",
                table: "MatchDetails",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
