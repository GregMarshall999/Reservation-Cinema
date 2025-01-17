namespace ReservationCinema.Dto
{
    public class CinemaDetailsDto
    {
        public CinemaDto Cinema { get; set; }
        public IEnumerable<FilmDto> Films { get; set; }
    }
}
