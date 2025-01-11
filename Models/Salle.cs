namespace ReservationCinema.Models
{
    public class Salle
    {
        public int Id { get; set; }
        public int Capacite { get; set; }
        public DateOnly DateConstr { get; set; }

        public int CinemaId { get; set; }

        public Cinema Cinema { get; set; }
        public ICollection<Seance> Seances { get; set; }
    }
}
