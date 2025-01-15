
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReservationCinema.Data;
using ReservationCinema.Models;

public class SalleController : Controller
{
    private readonly ApplicationDbContext _context;

    public SalleController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: SALLES
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Salles.ToListAsync());
    }

    // GET: SALLES/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salle = await _context.Salles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (salle == null)
        {
            return NotFound();
        }

        return View(salle);
    }

    // GET: SALLES/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: SALLES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ID,Title,ReleaseDate,Genre,Price")] Salle salle)
    {
        if (ModelState.IsValid)
        {
            _context.Add(salle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(salle);
    }

    // GET: SALLES/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salle = await _context.Salles.FindAsync(id);
        if (salle == null)
        {
            return NotFound();
        }
        return View(salle);
    }

    // POST: SALLES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ID,Title,ReleaseDate,Genre,Price")] Salle salle)
    {
        if (id != salle.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(salle);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalleExists(salle.Id))
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
        return View(salle);
    }

    // GET: SALLES/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var salle = await _context.Salles
            .FirstOrDefaultAsync(m => m.Id == id);
        if (salle == null)
        {
            return NotFound();
        }

        return View(salle);
    }

    // POST: SALLES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var salle = await _context.Salles.FindAsync(id);
        if (salle != null)
        {
            _context.Salles.Remove(salle);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SalleExists(int? id)
    {
        return _context.Salles.Any(e => e.Id == id);
    }
}
