using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using UpcomingGames.Database.Entities;

#nullable disable

namespace UpcomingGames.Database
{
    public partial class postgresContext : DbContext
    {
        public postgresContext()
        {
        }

        public postgresContext(DbContextOptions<postgresContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameCompany> GameCompanies { get; set; }
        public virtual DbSet<GameGenre> GameGenres { get; set; }
        public virtual DbSet<GamePlatform> GamePlatforms { get; set; }
        public virtual DbSet<GameTheme> GameThemes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Platform> Platforms { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=postgres;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.utf8");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("game");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CoverUrl).HasColumnName("cover_url");

                entity.Property(e => e.EsrbRating)
                    .HasMaxLength(16)
                    .HasColumnName("esrb_rating");

                entity.Property(e => e.IgdbId).HasColumnName("igdb_id");

                entity.Property(e => e.IsReleased).HasColumnName("is_released");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.PegiRating)
                    .HasMaxLength(16)
                    .HasColumnName("pegi_rating");

                entity.Property(e => e.ReleaseDate)
                    .IsRequired()
                    .HasColumnType("jsonb")
                    .HasColumnName("release_date");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Urls)
                    .HasColumnType("jsonb")
                    .HasColumnName("urls");
            });

            modelBuilder.Entity<GameCompany>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_company");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.Company)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_company_fk_1");

                entity.HasOne(d => d.Game)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_company_fk");
            });

            modelBuilder.Entity<GameGenre>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_genre");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.HasOne(d => d.Game)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_genre_fk");

                entity.HasOne(d => d.Genre)
                    .WithMany()
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_genre_fk_1");
            });

            modelBuilder.Entity<GamePlatform>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_platform");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.HasOne(d => d.Game)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_platform_fk");

                entity.HasOne(d => d.Platform)
                    .WithMany()
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_platform_fk_1");
            });

            modelBuilder.Entity<GameTheme>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_theme");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.ThemeId).HasColumnName("theme_id");

                entity.HasOne(d => d.Game)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_theme_fk");

                entity.HasOne(d => d.Theme)
                    .WithMany()
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_theme_fk_1");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Platform>(entity =>
            {
                entity.ToTable("platform");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Theme>(entity =>
            {
                entity.ToTable("theme");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
