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
    public class DocumentController : Controller
    {
        private readonly IdeaWebContext _context;

        public DocumentController(IdeaWebContext context)
        {
            _context = context;
        }

        // GET: Document
        public async Task<IActionResult> Index()
        {
            var ideaWebContext = _context.Document.Include(d => d.Idea);
            return View(await ideaWebContext.ToListAsync());
        }

        // GET: Document/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.Idea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // GET: Document/Create
        public IActionResult Create()
        {
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id");
            return View();
        }

        // POST: Document/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date_Upload,DataLink,IdeaId")] Document document)
        {
            if (ModelState.IsValid)
            {
                _context.Add(document);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", document.IdeaId);
            return View(document);
        }

        // GET: Document/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", document.IdeaId);
            return View(document);
        }

        // POST: Document/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date_Upload,DataLink,IdeaId")] Document document)
        {
            if (id != document.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(document);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DocumentExists(document.Id))
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
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", document.IdeaId);
            return View(document);
        }

        // GET: Document/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var document = await _context.Document
                .Include(d => d.Idea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (document == null)
            {
                return NotFound();
            }

            return View(document);
        }

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var document = await _context.Document.FindAsync(id);
            _context.Document.Remove(document);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DocumentExists(int id)
        {
            return _context.Document.Any(e => e.Id == id);
        }
    }
}
