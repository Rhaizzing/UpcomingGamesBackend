using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

namespace UpcomingGames.Database.Migrations
{
    public partial class FullTextNameSearch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"CREATE EXTENSION ""unaccent""");
            migrationBuilder.Sql("ALTER TEXT SEARCH CONFIGURATION english ALTER MAPPING FOR hword, hword_part, word WITH unaccent, english_stem;");
            
            migrationBuilder.AlterColumn<string>(
                name: "full_release_date",
                table: "game",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "game",
                type: "tsvector",
                nullable: true)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "name" });

            migrationBuilder.CreateIndex(
                name: "IX_game_SearchVector",
                table: "game",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP EXTENSION ""unaccent""");
            migrationBuilder.Sql("ALTER TEXT SEARCH CONFIGURATION english ALTER MAPPING FOR hword, hword_part, word WITH english_stem;");
            
            migrationBuilder.DropIndex(
                name: "IX_game_SearchVector",
                table: "game");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "game");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "full_release_date",
                table: "game",
                type: "date",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
