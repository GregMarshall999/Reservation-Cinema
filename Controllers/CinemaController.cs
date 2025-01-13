using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Services;

namespace ReservationCinema.Controllers
{
    public class CinemaController: Controller
    {
        private readonly CinemaService _cinemaService;

        public CinemaController(CinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        // Action pour afficher la page d'accueil avec la liste des cinémas
        public IActionResult Index()
        {
            var cinemas = _cinemaService.GetAllCinemas();
            return View(cinemas);  // Rendre la vue principale avec la liste des cinémas
        }

        // Action pour rechercher les cinémas via AJAX
        [HttpGet]
        public IActionResult SearchCinemas(string query)
        {
            var cinemas = _cinemaService.Search(query);
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_cinemaList", cinemas);  // Rendre une vue partielle de la liste des cinémas
            }
            return View(cinemas);  // Sinon, rendre la vue complète
        }

    }
}
