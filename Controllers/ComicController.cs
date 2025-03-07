using ComicSTore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ComicSTore.Models;

namespace ComicSTore.Controllers;

public class ComicController : Controller
{
    private readonly ApplicationDbContext _context;

    public ComicController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var comics = await _context.Comics.ToListAsync();
        return View(comics);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Title,Author,Publisher,Price,Description,CoverImageUrl")] Comic comic)
    {
        if (ModelState.IsValid)
        {
            _context.Add(comic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }
        }
        return View(comic);

    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return RedirectToAction(nameof(Index));

        var comic = await _context.Comics.FirstOrDefaultAsync(m => m.Id == id);
        if (comic == null) return RedirectToAction(nameof(Index));

        return View(comic);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return RedirectToAction(nameof(Index));

        var comic = await _context.Comics.FirstOrDefaultAsync(m => m.Id == id);
        if (comic == null) return RedirectToAction(nameof(Index));

        return View(comic);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Author,Price,Description,Publisher,CoverImageUrl")] Comic comic)
    {
        if (id != comic.Id) return RedirectToAction(nameof(Index));

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(comic);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ComicExists(comic.Id))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(comic);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return RedirectToAction(nameof(Index));

        var comic = await _context.Comics.FirstOrDefaultAsync(m => m.Id == id);
        if (comic == null) return RedirectToAction(nameof(Index));

        return View(comic);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var comic = await _context.Comics.FindAsync(id);
        if (comic != null)
        {
            _context.Comics.Remove(comic);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ComicExists(int id)
    {
        return await _context.Comics.AnyAsync(e => e.Id == id);
    }
}
