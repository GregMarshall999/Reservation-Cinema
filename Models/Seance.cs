namespace ReservationCinema.Models
{
    public class Seance
    {
        public int Id { get; set; }
        public float Tarif { get; set; }
        
        public int FilmId { get; set; }
        public int HoraireId { get; set; }
        public int SalleId { get; set; }

        public Film Film { get; set; }
        public Horaire Horaire { get; set; }
        public Salle Salle { get; set; }
    }
}
