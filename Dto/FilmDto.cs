namespace ReservationCinema.Dto
{
    public class FilmDto
    {
        public string Titre { get; set; }
        public string Genre { get; set; }
        public string Annee { get; set; }
        public List<SeanceDto> Seances { get; set; }
    }
}
