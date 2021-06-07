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

        public virtual DbSet<CompanyEntity> Companies { get; set; }
        public virtual DbSet<GameEntity> Games { get; set; }
        public virtual DbSet<GameCompanyEntity> GameCompanies { get; set; }
        public virtual DbSet<GameGenreEntity> GameGenres { get; set; }
        public virtual DbSet<GamePlatformEntity> GamePlatforms { get; set; }
        public virtual DbSet<GameThemeEntity> GameThemes { get; set; }
        public virtual DbSet<GenreEntity> Genres { get; set; }
        public virtual DbSet<PlatformEntity> Platforms { get; set; }
        public virtual DbSet<ThemeEntity> Themes { get; set; }

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

            modelBuilder.Entity<CompanyEntity>(entity =>
            {
                entity.ToTable("company");

                entity.Property(e => e.Id).HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.LogoUrl).HasColumnName("logo_url");

                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<GameEntity>(entity =>
            {
                entity.ToTable("game");
                
                entity.Property(e => e.Id).HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

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
                
                entity.Property(e => e.FullReleaseDate)
                    .HasColumnType("jsonb")
                    .HasColumnName("full_release_date");

                entity.Property(e => e.Score).HasColumnName("score");

                entity.Property(e => e.Urls)
                    .HasColumnType("jsonb")
                    .HasColumnName("urls");
                
                entity.HasIndex(e => e.Name);
                entity.HasIndex(e => e.IgdbId);
                entity.HasIndex(e => e.IsReleased);
                entity.HasIndex(e => e.ReleaseDate);
                entity.HasIndex(e => e.FullReleaseDate);

            });

            modelBuilder.Entity<GameCompanyEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_company");

                entity.Property(e => e.CompanyId).HasColumnName("company_id");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.HasOne(d => d.CompanyEntity)
                    .WithMany()
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_company_fk_1");

                entity.HasOne(d => d.GameEntity)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_company_fk");
            });

            modelBuilder.Entity<GameGenreEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_genre");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.HasOne(d => d.GameEntity)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_genre_fk");

                entity.HasOne(d => d.GenreEntity)
                    .WithMany()
                    .HasForeignKey(d => d.GenreId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_genre_fk_1");
            });

            modelBuilder.Entity<GamePlatformEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_platform");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.PlatformId).HasColumnName("platform_id");

                entity.HasOne(d => d.GameEntity)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_platform_fk");

                entity.HasOne(d => d.PlatformEntity)
                    .WithMany()
                    .HasForeignKey(d => d.PlatformId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_platform_fk_1");
            });

            modelBuilder.Entity<GameThemeEntity>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("game_theme");

                entity.Property(e => e.GameId).HasColumnName("game_id");

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.ThemeId).HasColumnName("theme_id");

                entity.HasOne(d => d.GameEntity)
                    .WithMany()
                    .HasForeignKey(d => d.GameId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_theme_fk");

                entity.HasOne(d => d.ThemeEntity)
                    .WithMany()
                    .HasForeignKey(d => d.ThemeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("game_theme_fk_1");
            });

            modelBuilder.Entity<GenreEntity>(entity =>
            {
                entity.ToTable("genre");

                entity.Property(e => e.Id).HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
                
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<PlatformEntity>(entity =>
            {
                entity.ToTable("platform");

                entity.Property(e => e.Id).HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
                
                entity.HasIndex(e => e.Name).IsUnique();
            });

            modelBuilder.Entity<ThemeEntity>(entity =>
            {
                entity.ToTable("theme");

                entity.Property(e => e.Id).HasColumnName("id").UseSerialColumn().ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");
                
                entity.HasIndex(e => e.Name).IsUnique();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
