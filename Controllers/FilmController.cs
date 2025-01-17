using Microsoft.AspNetCore.Mvc;
using ReservationCinema.Dto;
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

        public IActionResult Index2(string query, int page = 1)
        {
            // Appeler le service pour récupérer les films paginés
            var paginatedFilms = _filmService.SearchFilms(query, page);

            // Retourner la vue avec le modèle Paginated<FilmDto>
            return View(paginatedFilms);
        }
    }
}
