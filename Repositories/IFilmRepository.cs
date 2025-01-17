using ReservationCinema.Dto;
using ReservationCinema.Models;

namespace ReservationCinema.Repositories
{
    public interface IFilmRepository
    {
        PaginationDto<List<Film>> SearchFilms(string query, int page);
        IEnumerable<Film> GetFilmsForCinemaToday(int cinemaId);
    }
}
