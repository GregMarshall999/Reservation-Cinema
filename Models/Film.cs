namespace ReservationCinema.Models
{
    public class Film
    {
        public int Id { get; set; }
        public string Titre { get; set; }
        public DateOnly Annee { get; set; }
        public string Genre { get; set; }
        public string ImageUrl { get; set; }

        public ICollection<Seance> Seances { get; set; }
    }
}
