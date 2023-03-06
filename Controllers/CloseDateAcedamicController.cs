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
    public class CloseDateAcedamicController : Controller
    {
        private readonly IdeaWebContext _context;

        public CloseDateAcedamicController(IdeaWebContext context)
        {
            _context = context;
        }

        // GET: CloseDateAcedamic
        public async Task<IActionResult> Index()
        {
            return View(await _context.CloseDateAcedamic.ToListAsync());
        }

        // GET: CloseDateAcedamic/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: CloseDateAcedamic/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CloseDateAcedamic/Create
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

        // GET: CloseDateAcedamic/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

        // POST: CloseDateAcedamic/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CloseDate,CloseDatePostIdea")] CloseDateAcedamic closeDateAcedamic)
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

        // GET: CloseDateAcedamic/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: CloseDateAcedamic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var closeDateAcedamic = await _context.CloseDateAcedamic.FindAsync(id);
            _context.CloseDateAcedamic.Remove(closeDateAcedamic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CloseDateAcedamicExists(int id)
        {
            return _context.CloseDateAcedamic.Any(e => e.Id == id);
        }
    }
}