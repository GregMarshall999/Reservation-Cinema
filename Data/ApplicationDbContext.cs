using Microsoft.EntityFrameworkCore;
using ReservationCinema.Models;

namespace ReservationCinema.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Cinema> Cinemas { get; set; }
        public DbSet<Salle> Salles { get; set; }
        public DbSet<Horaire> Horaires { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Film> Films { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Film)
                .WithMany(f => f.Seances)
                .HasForeignKey(s => s.FilmId);

            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Horaire)
                .WithMany(f => f.Seances)
                .HasForeignKey(s => s.HoraireId);

            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Salle)
                .WithMany(f => f.Seances)
                .HasForeignKey(s => s.SalleId);

            modelBuilder.Entity<Salle>()
                .HasOne(s => s.Cinema)
                .WithMany(f => f.Salles)
                .HasForeignKey(s => s.CinemaId);
        }
    }
}
