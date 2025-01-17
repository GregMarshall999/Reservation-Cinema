namespace ReservationCinema.Dto
{
    public class SalleDto
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public int Capacite { get; set; }
        public CinemaDto Cinema { get; set; }
    }
}
