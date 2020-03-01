using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class ChangeTeamStatisticsDbName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamStatisticses_LeagueTables_LeagueTableId",
                table: "TeamStatisticses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamStatisticses",
                table: "TeamStatisticses");

            migrationBuilder.RenameTable(
                name: "TeamStatisticses",
                newName: "TeamStatistics");

            migrationBuilder.RenameIndex(
                name: "IX_TeamStatisticses_LeagueTableId",
                table: "TeamStatistics",
                newName: "IX_TeamStatistics_LeagueTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamStatistics",
                table: "TeamStatistics",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStatistics_LeagueTables_LeagueTableId",
                table: "TeamStatistics",
                column: "LeagueTableId",
                principalTable: "LeagueTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamStatistics_LeagueTables_LeagueTableId",
                table: "TeamStatistics");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TeamStatistics",
                table: "TeamStatistics");

            migrationBuilder.RenameTable(
                name: "TeamStatistics",
                newName: "TeamStatisticses");

            migrationBuilder.RenameIndex(
                name: "IX_TeamStatistics_LeagueTableId",
                table: "TeamStatisticses",
                newName: "IX_TeamStatisticses_LeagueTableId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TeamStatisticses",
                table: "TeamStatisticses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamStatisticses_LeagueTables_LeagueTableId",
                table: "TeamStatisticses",
                column: "LeagueTableId",
                principalTable: "LeagueTables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
