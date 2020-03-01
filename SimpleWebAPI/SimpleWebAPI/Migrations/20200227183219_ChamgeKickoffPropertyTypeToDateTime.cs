﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleWebAPI.Migrations
{
    public partial class ChamgeKickoffPropertyTypeToDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "KickoffAt",
                table: "MatchDetails",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "KickoffAt",
                table: "MatchDetails",
                type: "datetimeoffset",
                nullable: false,
                oldClrType: typeof(DateTime));
        }
    }
}
