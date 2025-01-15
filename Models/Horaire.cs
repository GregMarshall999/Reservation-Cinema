namespace ReservationCinema.Models
{
    public class Horaire
    {
        public int Id { get; set; }
        public DateTime HeureDebut { get; set; }
        public DateTime HeureFin { get; set; }

        public ICollection<Seance>? Seances { get; set; }
    }
}
