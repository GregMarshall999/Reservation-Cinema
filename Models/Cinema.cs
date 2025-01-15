namespace ReservationCinema.Models
{
    public class Cinema
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Ville { get; set; }
        public string Rue { get; set; }
        public string Numero { get; set; }

        public ICollection<Salle>? Salles { get; set; }
    }
}
