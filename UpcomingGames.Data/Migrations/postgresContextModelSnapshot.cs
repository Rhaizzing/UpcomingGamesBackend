// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UpcomingGames.Database;

namespace UpcomingGames.Database.Migrations
{
    [DbContext(typeof(postgresContext))]
    partial class postgresContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "en_US.utf8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "6.0.0-preview.4.21253.1")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("UpcomingGames.Data.Entities.CompanyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("LogoUrl")
                        .HasColumnType("text")
                        .HasColumnName("logo_url");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("company");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameCompanyEntity", b =>
                {
                    b.Property<int>("CompanyId")
                        .HasColumnType("integer")
                        .HasColumnName("company_id");

                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.HasIndex("CompanyId");

                    b.HasIndex("GameId");

                    b.ToTable("game_company");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("CoverUrl")
                        .HasColumnType("text")
                        .HasColumnName("cover_url");

                    b.Property<string>("EsrbRating")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("esrb_rating");

                    b.Property<DateOnly?>("FullReleaseDate")
                        .HasColumnType("date")
                        .HasColumnName("full_release_date");

                    b.Property<long>("IgdbId")
                        .HasColumnType("bigint")
                        .HasColumnName("igdb_id");

                    b.Property<bool>("IsReleased")
                        .HasColumnType("boolean")
                        .HasColumnName("is_released");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.Property<string>("PegiRating")
                        .HasMaxLength(16)
                        .HasColumnType("character varying(16)")
                        .HasColumnName("pegi_rating");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("jsonb")
                        .HasColumnName("release_date");

                    b.Property<double?>("Score")
                        .HasColumnType("double precision")
                        .HasColumnName("score");

                    b.Property<string>("Urls")
                        .HasColumnType("jsonb")
                        .HasColumnName("urls");

                    b.HasKey("Id");

                    b.HasIndex("FullReleaseDate");

                    b.HasIndex("IgdbId");

                    b.HasIndex("IsReleased");

                    b.HasIndex("Name");

                    b.HasIndex("ReleaseDate");

                    b.ToTable("game");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameGenreEntity", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("GenreId")
                        .HasColumnType("integer")
                        .HasColumnName("genre_id");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.HasIndex("GameId");

                    b.HasIndex("GenreId");

                    b.ToTable("game_genre");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GamePlatformEntity", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("PlatformId")
                        .HasColumnType("integer")
                        .HasColumnName("platform_id");

                    b.HasIndex("GameId");

                    b.HasIndex("PlatformId");

                    b.ToTable("game_platform");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameThemeEntity", b =>
                {
                    b.Property<int>("GameId")
                        .HasColumnType("integer")
                        .HasColumnName("game_id");

                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int>("ThemeId")
                        .HasColumnType("integer")
                        .HasColumnName("theme_id");

                    b.HasIndex("GameId");

                    b.HasIndex("ThemeId");

                    b.ToTable("game_theme");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GenreEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("genre");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.PlatformEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("platform");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.ThemeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("theme");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameCompanyEntity", b =>
                {
                    b.HasOne("UpcomingGames.Data.Entities.CompanyEntity", "CompanyEntity")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .HasConstraintName("game_company_fk_1")
                        .IsRequired();

                    b.HasOne("UpcomingGames.Data.Entities.GameEntity", "GameEntity")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_company_fk")
                        .IsRequired();

                    b.Navigation("CompanyEntity");

                    b.Navigation("GameEntity");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameGenreEntity", b =>
                {
                    b.HasOne("UpcomingGames.Data.Entities.GameEntity", "GameEntity")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_genre_fk")
                        .IsRequired();

                    b.HasOne("UpcomingGames.Data.Entities.GenreEntity", "GenreEntity")
                        .WithMany()
                        .HasForeignKey("GenreId")
                        .HasConstraintName("game_genre_fk_1")
                        .IsRequired();

                    b.Navigation("GameEntity");

                    b.Navigation("GenreEntity");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GamePlatformEntity", b =>
                {
                    b.HasOne("UpcomingGames.Data.Entities.GameEntity", "GameEntity")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_platform_fk")
                        .IsRequired();

                    b.HasOne("UpcomingGames.Data.Entities.PlatformEntity", "PlatformEntity")
                        .WithMany()
                        .HasForeignKey("PlatformId")
                        .HasConstraintName("game_platform_fk_1")
                        .IsRequired();

                    b.Navigation("GameEntity");

                    b.Navigation("PlatformEntity");
                });

            modelBuilder.Entity("UpcomingGames.Data.Entities.GameThemeEntity", b =>
                {
                    b.HasOne("UpcomingGames.Data.Entities.GameEntity", "GameEntity")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("game_theme_fk")
                        .IsRequired();

                    b.HasOne("UpcomingGames.Data.Entities.ThemeEntity", "ThemeEntity")
                        .WithMany()
                        .HasForeignKey("ThemeId")
                        .HasConstraintName("game_theme_fk_1")
                        .IsRequired();

                    b.Navigation("GameEntity");

                    b.Navigation("ThemeEntity");
                });
#pragma warning restore 612, 618
        }
    }
}
