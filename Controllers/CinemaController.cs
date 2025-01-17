using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

       
       //public IActionResult Index()
       // {
           
       // }

        }
}
