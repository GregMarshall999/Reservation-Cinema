using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ReservationCinema.Models;
using ReservationCinema.Services;
using ReservationCinema.Dto;
namespace ReservationCinema.Controllers
{
    public class HomeController : Controller
    {

        private readonly CinemaService _cinemaService;
        private readonly FilmService _filmService;

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, CinemaService cinemaService, FilmService filmService)
        {
            _cinemaService = cinemaService;
            _filmService = filmService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index(string query, int page = 1, int pageSize = 4)
        {
            // Récupérer les résultats paginés
            var result = _cinemaService.GetPaginatedCinemas(query, page, pageSize);

            // Ajouter des informations supplémentaires nécessaires à la vue via ViewBag
            ViewBag.SearchQuery = query;

            return View(result);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpGet]
        //public IActionResult Details(int id)
        //{
        //    // Récupérer les détails du cinéma par ID
        //    var cinemaDto = _cinemaService.GetCinemaById(id);

        //    if (cinemaDto == null)
        //    {
        //        return NotFound();
        //    }

        //    // Récupérer les films programmés pour ce cinéma
        //    var films = _filmService.GetFilmsForCinemaToday(id);

        //    var cinemaDetailsDto = new CinemaDetailsDto
        //    {
        //        Cinema = cinemaDto,
        //        Films = films
        //    };

        //    return View(cinemaDetailsDto);
        //}
    }
}

