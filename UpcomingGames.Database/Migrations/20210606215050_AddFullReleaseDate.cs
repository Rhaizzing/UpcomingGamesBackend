using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace UpcomingGames.Database.Migrations
{
    public partial class AddFullReleaseDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "full_release_date",
                table: "game",
                type: "date",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "full_release_date",
                table: "game");
        }
    }
}
