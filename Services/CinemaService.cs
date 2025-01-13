using ReservationCinema.Models;
using ReservationCinema.Repositories;

namespace ReservationCinema.Services
{
    public class CinemaService
    {
        private readonly ICinemaRepository _cinemaRepository;

        public CinemaService(ICinemaRepository cinemaRepository)
        {
            _cinemaRepository = cinemaRepository;
        }

        public IEnumerable<Cinema> GetAllCinemas()
        {
            return _cinemaRepository.GetAll();
        }

        public IEnumerable<Cinema> Search(string query)
        {
            return _cinemaRepository.Search(query);
        }

    }
}