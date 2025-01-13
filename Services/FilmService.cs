using ReservationCinema.Models;
using ReservationCinema.Repositories;

namespace ReservationCinema.Services
{
    public class FilmService
    {
        private readonly IRepository<Film> _filmRepository;

        public FilmService(IRepository<Film> filmRepository)
        {
            _filmRepository = filmRepository;
        }

        public IEnumerable<Film> GetAllFilms()
        {
            return _filmRepository.GetAll();
        }
    }
}
