using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Data;
using IdeaWeb.Models;

namespace IdeaWeb.Controllers
{
    public class CloseDateAcedamicsController : Controller
    {
        private readonly IdeaWebContext _context;

        public CloseDateAcedamicsController(IdeaWebContext context)
        {
            _context = context;
        }

        // GET: CloseDateAcedamics
        public async Task<IActionResult> Index()
        {
            return View(await _context.CloseDateAcedamic.ToListAsync());
        }

        // GET: CloseDateAcedamics/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var closeDateAcedamic = await _context.CloseDateAcedamic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (closeDateAcedamic == null)
            {
                return NotFound();
            }

            return View(closeDateAcedamic);
        }

        // GET: CloseDateAcedamics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CloseDateAcedamics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CloseDate,CloseDatePostIdea")] CloseDateAcedamic closeDateAcedamic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(closeDateAcedamic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(closeDateAcedamic);
        }

        // GET: CloseDateAcedamics/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var closeDateAcedamic = await _context.CloseDateAcedamic.FindAsync(id);
            if (closeDateAcedamic == null)
            {
                return NotFound();
            }
            return View(closeDateAcedamic);
        }

        // POST: CloseDateAcedamics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,CloseDate,CloseDatePostIdea")] CloseDateAcedamic closeDateAcedamic)
        {
            if (id != closeDateAcedamic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(closeDateAcedamic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CloseDateAcedamicExists(closeDateAcedamic.Id))
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
            return View(closeDateAcedamic);
        }

        // GET: CloseDateAcedamics/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var closeDateAcedamic = await _context.CloseDateAcedamic
                .FirstOrDefaultAsync(m => m.Id == id);
            if (closeDateAcedamic == null)
            {
                return NotFound();
            }

            return View(closeDateAcedamic);
        }

        // POST: CloseDateAcedamics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var closeDateAcedamic = await _context.CloseDateAcedamic.FindAsync(id);
            _context.CloseDateAcedamic.Remove(closeDateAcedamic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CloseDateAcedamicExists(string id)
        {
            return _context.CloseDateAcedamic.Any(e => e.Id == id);
        }
    }
}
