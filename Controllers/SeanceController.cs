
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Models;

public class SeanceController : Controller
{
    private readonly Context _context;

    public SeanceController(Context context)
    {
        _context = context;
    }

    // GET: SEANCES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Seance.ToListAsync());
    }

    // GET: SEANCES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var seance = await _context.Seance
            .FirstOrDefaultAsync(m => m.Id == id);
        if (seance == null)
        {
            return NotFound();
        }

        return View(seance);
    }

    // GET: SEANCES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SEANCES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Seance movie)
    {
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

        var seance = await _context.Seance.FindAsync(id);
        if (seance == null)
        {
            return NotFound();
        }
        return View(seance);
    }

    // POST: SEANCES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Seance movie)
    {
        if (id != seance.Id)
        {
            return NotFound();
        }

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

        var seance = await _context.Seance
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
        var seance = await _context.Seance.FindAsync(id);
        if (seance != null)
        {
            _context.Seance.Remove(seance);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SeanceExists(int? id)
    {
        return _context.Seance.Any(e => e.Id == id);
    }
}
