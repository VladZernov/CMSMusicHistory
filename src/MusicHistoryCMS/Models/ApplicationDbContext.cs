using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace MusicHistoryCMS.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Article>(entity =>
            {
                entity.Property(e => e.AuthorID)
                    .IsRequired()
                    .HasMaxLength(450);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Author).WithMany(p => p.Articles).HasForeignKey(d => d.AuthorID);

                entity.HasOne(d => d.Issue).WithMany(p => p.Articles).HasForeignKey(d => d.IssueID).OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Subject).WithMany(p => p.Articles).HasForeignKey(d => d.SubjectID);
            });

            modelBuilder.Entity<Composer>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("Композитор$КомпозиторКомпозитор");

                entity.HasIndex(e => e.PeriodID).HasName("Композитор$ЭпохаКомпозитор");

                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Subject).WithOne(p => p.Composer).HasForeignKey<Composer>(d => d.ID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Period).WithMany(p => p.Composers).HasForeignKey(d => d.PeriodID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Composition>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.KeywordID).HasDefaultValue(0);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.YoutubeKey).HasMaxLength(255);

                entity.HasOne(d => d.Composer).WithMany(p => p.Compositions).HasForeignKey(d => d.ComposerID);

                entity.HasOne(d => d.Genre).WithMany(p => p.Compositions).HasForeignKey(d => d.GenreID);

                entity.HasOne(d => d.Subject).WithOne(p => p.Composition).HasForeignKey<Composition>(d => d.ID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Keyword).WithMany(p => p.Compositions).HasForeignKey(d => d.KeywordID);
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.Subject).WithOne(p => p.Genre).HasForeignKey<Genre>(d => d.ID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Instrument>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Subject).WithOne(p => p.Instrument).HasForeignKey<Instrument>(d => d.ID).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.Period).WithMany(p => p.Instruments).HasForeignKey(d => d.PeriodID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<Keyword>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("Ключевое_слово$Ключевое_словоКлючевое_слово");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Period>(entity =>
            {
                entity.Property(e => e.ID).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.HasOne(d => d.Subject).WithOne(p => p.Period).HasForeignKey<Period>(d => d.ID).OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255);
            });
        }

        public virtual DbSet<Article> Article { get; set; }
        public virtual DbSet<Composer> Composer { get; set; }
        public virtual DbSet<Composition> Composition { get; set; }
        public virtual DbSet<Genre> Genre { get; set; }
        public virtual DbSet<Instrument> Instrument { get; set; }
        public virtual DbSet<Issue> Issue { get; set; }
        public virtual DbSet<Keyword> Keyword { get; set; }
        public virtual DbSet<Period> Period { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
    }
}
