using Microsoft.AspNetCore.Mvc;
using ReservationCinema.Models;
using ReservationCinema.Services;

namespace ReservationCinema.Controllers
{
    public class CinemaController : Controller
    {
        private readonly IGenericService<Cinema> _cinemaService;

        public CinemaController(IGenericService<Cinema> cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public async Task<IActionResult> Index()
        {
            var cinemas = await _cinemaService.GetAll();

            return View(cinemas);
        }

        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cinema cinema) 
        {
            if (ModelState.IsValid) 
            {
                await _cinemaService.Add(cinema);
                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var cinema = await _cinemaService.GetById(id);

            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                await _cinemaService.Update(cinema);
                return RedirectToAction(nameof(Index));
            }

            return View(cinema);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var cinema = await _cinemaService.GetById(id);

            if (cinema == null)
            {
                return NotFound();
            }

            return View(cinema);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _cinemaService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
