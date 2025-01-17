using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Dto;
using ReservationCinema.Services;

namespace ReservationCinema.Controllers
{
    public class CinemaController: Controller
    {
        private readonly CinemaService _cinemaService;
        private readonly FilmService _filmService;

        public CinemaController(CinemaService cinemaService, FilmService filmService)
        {
            _cinemaService = cinemaService;
            _filmService = filmService;
        }
        public IActionResult Details(int id)
        {
            // Récupérer les détails du cinéma par ID
            var cinemaDto = _cinemaService.GetCinemaById(id);

            if (cinemaDto == null)
            {
                return NotFound();
            }

            // Récupérer les films programmés pour ce cinéma
            var films = _filmService.GetFilmsForCinemaToday(id);

            var cinemaDetailsDto = new CinemaDetailsDto
            {
                Cinema = cinemaDto,
                Films = films
            };

            return View(cinemaDetailsDto);
        }

    }
}
