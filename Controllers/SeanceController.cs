
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ReservationCinema.Data;
using ReservationCinema.Models;

public class SeanceController : Controller
{
    private readonly ApplicationDbContext _context;

    public SeanceController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: SEANCES
    public async Task<IActionResult> Index()    
    {
        var seance = await _context.Seances.ToListAsync();

        foreach (var item in seance)
        {
            item.Film = await _context.Films.FindAsync(item.FilmId);
            item.Horaire = await _context.Horaires.FindAsync(item.HoraireId);
            item.Salle = await _context.Salles.FindAsync(item.SalleId);
        }

        return View(seance);
    }

    // GET: SEANCES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seance = await _context.Seances
            .FirstOrDefaultAsync(m => m.Id == id);
        if (seance == null)
        {
            return NotFound();
        }

        seance.Film = await _context.Films.FindAsync(seance.FilmId);
        seance.Horaire = await _context.Horaires.FindAsync(seance.HoraireId);
        seance.Salle = await _context.Salles.FindAsync(seance.SalleId);

        return View(seance);
    }

    // GET: SEANCES/Create
    public IActionResult Create()
    {
        ViewBag.Films = new SelectList(_context.Films, "Id", "Titre");
        ViewBag.Horaires = new SelectList(_context.Horaires, "Id", "HeureDebut");
        ViewBag.Salles = new SelectList(_context.Salles, "Id", "Capacite");
        return View();
    }

    // POST: SEANCES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Tarif,FilmId,HoraireId,SalleId")] Seance seance)
    {
        seance.Film = await _context.Films.FindAsync(seance.FilmId);
        seance.Horaire = await _context.Horaires.FindAsync(seance.HoraireId);
        seance.Salle = await _context.Salles.FindAsync(seance.SalleId);
        var invalid = false;

        if (seance.Film == null)
        {
            ModelState.AddModelError("FilmId", "Invalid Film Id.");
            invalid = true;
        }
        if (seance.Horaire == null)
        {
            ModelState.AddModelError("HoraireId", "Invalid Horaire Id.");
            invalid = true;
        }
        if (seance.Salle == null)
        {
            ModelState.AddModelError("SalleId", "Invalid Salle Id.");
            invalid = true;
        }

        if (invalid) 
        {
            return View(seance);
        }

        ModelState.Remove("Film");
        ModelState.Remove("Horaire");
        ModelState.Remove("Salle");

        if (ModelState.IsValid)
        {
            _context.Add(seance);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(seance);
    }

    // GET: SEANCES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seance = await _context.Seances.FindAsync(id);
        if (seance == null)
        {
            return NotFound();
        }

        ViewBag.Films = new SelectList(_context.Films, "Id", "Titre", seance.FilmId);
        ViewBag.Horaires = new SelectList(_context.Horaires, "Id", "HeureDebut", seance.HoraireId);
        ViewBag.Salles = new SelectList(_context.Salles, "Id", "Capacite", seance.SalleId);
        return View(seance);
    }

    // POST: SEANCES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,Tarif,FilmId,HoraireId,SalleId")] Seance seance)
    {
        if (id != seance.Id)
        {
            return NotFound();
        }

        seance.Film = await _context.Films.FindAsync(seance.FilmId);
        seance.Horaire = await _context.Horaires.FindAsync(seance.HoraireId);
        seance.Salle = await _context.Salles.FindAsync(seance.SalleId);
        var invalid = false;

        if (seance.Film == null)
        {
            ModelState.AddModelError("FilmId", "Invalid Film Id.");
            invalid = true;
        }
        if (seance.Horaire == null)
        {
            ModelState.AddModelError("HoraireId", "Invalid Horaire Id.");
            invalid = true;
        }
        if (seance.Salle == null)
        {
            ModelState.AddModelError("SalleId", "Invalid Salle Id.");
            invalid = true;
        }

        if (invalid)
        {
            return View(seance);
        }

        ModelState.Remove("Film");
        ModelState.Remove("Horaire");
        ModelState.Remove("Salle");

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(seance);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeanceExists(seance.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(seance);
    }

    // GET: SEANCES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seance = await _context.Seances
            .FirstOrDefaultAsync(m => m.Id == id);
        if (seance == null)
        {
            return NotFound();
        }

        return View(seance);
    }

    // POST: SEANCES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var seance = await _context.Seances.FindAsync(id);
        if (seance != null)
        {
            _context.Seances.Remove(seance);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SeanceExists(int? id)
    {
        return _context.Seances.Any(e => e.Id == id);
    }
}
