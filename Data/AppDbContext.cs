using Microsoft.EntityFrameworkCore;
using Shared.Project.Entities;

namespace Server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Adressen> adressen { get; set; }
        public DbSet<Mitarbeiterdevice> mitarbeiterdevice { get; set; }
        public DbSet<Projekt> Projekt { get; set; }

        public DbSet<Artikel> Artikel { get; set; }
        public DbSet<Fremdleistung> Fremdleistungens { get; set; }

        public DbSet<Leistungen> Leistungens { get; set; }

        public DbSet<FileSystemEntity> FileSystemEntities { get; set; }


        public DbSet<UploadResult> Uploads => Set<UploadResult>();

        public DbSet<Lohnarten> Lohnartens { get; set; }
       public DbSet<Ansprechpartner> Ansprechpartner { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define composite key for Adressen entity
            modelBuilder.Entity<Adressen>()
                .HasKey(e => new { e.Mandant, e.Adresse });

            modelBuilder.Entity<Ansprechpartner>()
               .HasKey(e => new { e.Mandant, e.Pos });

            // Define composite key for Projekt entity
            modelBuilder.Entity<Projekt>()
                .HasKey(e => new { e.Mandant, e.Projektnr });

            // Define composite key for Mitarbeiter entity
            modelBuilder.Entity<Mitarbeiterdevice>()
                .HasKey(e => new { e.Mandant, e.Bezeichnung });

            // Define composite key for Ansprechpartner entity
            modelBuilder.Entity<Ansprechpartner>()
                .HasKey(e => new { e.Mandant, e.Pos });

            modelBuilder.Entity<Artikel>()
               .HasKey(e => new { e.Mandant, e.Artikelnummer });

            modelBuilder.Entity<Lohnarten>()
               .HasKey(e => new { e.Mandant, e.Lohnart });

            modelBuilder.Entity<Fremdleistung>()
              .HasKey(e => new { e.Mandant, e.Nummer });

            modelBuilder.Entity<Leistungen>()
            .HasKey(e => new { e.Mandant, e.Leistung });

            // Optionally, configure other entity mappings or relationships here
        }



    }
}
