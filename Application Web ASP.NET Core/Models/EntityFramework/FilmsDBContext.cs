using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application_Web_ASP.NET_Core.Models.EntityFramework
{
    public class FilmsDBContext : DbContext
    {
        public FilmsDBContext()
        {
        }

        public FilmsDBContext(DbContextOptions<FilmsDBContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Compte> Compte { get; set; }
        public virtual DbSet<Favori> Favori { get; set; }
        public virtual DbSet<Film> Film { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.EnableSensitiveDataLogging().UseNpgsql("Server=localhost;port=5432;Database=FilmsDBTP3;uid=postgres; password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Favori>(entity =>
            {
                entity.HasOne(f => f.FilmNavigation)
                      .WithMany(f => f.Favoris)
                      .HasForeignKey(f => f.FilmId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_fav_fil");

                entity.HasOne(f => f.CompteNavigation)
                      .WithMany(f => f.Favoris)
                      .HasForeignKey(f => f.CompteId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("fk_fav_com");

                entity.HasKey(e => new { e.FilmId, e.CompteId }).HasName("pk_fav");
            });

            modelBuilder.Entity<Compte>(entity =>
            {
                entity.HasIndex(c => c.Mel).IsUnique();
                entity.Property(c => c.DateCreation).HasDefaultValueSql("current_date");
            });


       

        }
    }
}
