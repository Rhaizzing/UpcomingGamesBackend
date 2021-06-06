using Microsoft.EntityFrameworkCore.Migrations;

namespace UpcomingGames.Database.Migrations
{
    public partial class AddIndexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_theme_name",
                table: "theme",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_platform_name",
                table: "platform",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_genre_name",
                table: "genre",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_game_full_release_date",
                table: "game",
                column: "full_release_date");

            migrationBuilder.CreateIndex(
                name: "IX_game_igdb_id",
                table: "game",
                column: "igdb_id");

            migrationBuilder.CreateIndex(
                name: "IX_game_is_released",
                table: "game",
                column: "is_released");

            migrationBuilder.CreateIndex(
                name: "IX_game_name",
                table: "game",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_game_release_date",
                table: "game",
                column: "release_date");

            migrationBuilder.CreateIndex(
                name: "IX_company_name",
                table: "company",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_theme_name",
                table: "theme");

            migrationBuilder.DropIndex(
                name: "IX_platform_name",
                table: "platform");

            migrationBuilder.DropIndex(
                name: "IX_genre_name",
                table: "genre");

            migrationBuilder.DropIndex(
                name: "IX_game_full_release_date",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_igdb_id",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_is_released",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_name",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_game_release_date",
                table: "game");

            migrationBuilder.DropIndex(
                name: "IX_company_name",
                table: "company");
        }
    }
}
