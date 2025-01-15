
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Models;

public class HoraireController : Controller
{
    private readonly Context _context;

    public HoraireController(Context context)
    {
        _context = context;
    }

    // GET: HORAIRES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Horaire.ToListAsync());
    }

    // GET: HORAIRES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var horaire = await _context.Horaire
            .FirstOrDefaultAsync(m => m.Id == id);
        if (horaire == null)
        {
            return NotFound();
        }

        return View(horaire);
    }

    // GET: HORAIRES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: HORAIRES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Horaire movie)
    {
        if (ModelState.IsValid)
        {
            _context.Add(horaire);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(horaire);
    }

    // GET: HORAIRES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var horaire = await _context.Horaire.FindAsync(id);
        if (horaire == null)
        {
            return NotFound();
        }
        return View(horaire);
    }

    // POST: HORAIRES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Horaire movie)
    {
        if (id != horaire.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(horaire);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HoraireExists(horaire.Id))
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
        return View(horaire);
    }

    // GET: HORAIRES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var horaire = await _context.Horaire
            .FirstOrDefaultAsync(m => m.Id == id);
        if (horaire == null)
        {
            return NotFound();
        }

        return View(horaire);
    }

    // POST: HORAIRES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var horaire = await _context.Horaire.FindAsync(id);
        if (horaire != null)
        {
            _context.Horaire.Remove(horaire);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool HoraireExists(int? id)
    {
        return _context.Horaire.Any(e => e.Id == id);
    }
}
