﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace ItIsPizzaDay.Server.Migrations
{
    public partial class muppetisAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "muppet",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "muppet");
        }
    }
}
