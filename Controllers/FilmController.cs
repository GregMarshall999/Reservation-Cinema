using Microsoft.AspNetCore.Mvc;
using ReservationCinema.Services;

namespace ReservationCinema.Controllers
{
    public class FilmController : Controller
    {
        private readonly FilmService _filmService;

        public FilmController(FilmService filmService)
        {
            _filmService = filmService;
        }

        public IActionResult Index()
        {
            var films = _filmService.GetAllFilms();
            return View(films); // Passe les films à la vue
        }
    }
}
