using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IdeaWeb.Data;
using IdeaWeb.Models;
using System.IO.Compression;

namespace IdeaWeb.Controllers
{
    public class IdeaController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly IdeaWebContext _context;
        public IdeaController(IdeaWebContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        // GET: Idea
        public async Task<IActionResult> Index()
        {
            var ideaWebContext = _context.Idea.Include(i => i.Category).Include(i => i.User);
            return View(await ideaWebContext.ToListAsync());
        }

        // GET: Idea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .Include(i => i.Category)
                .Include(i => i.Comments)
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.item = _context.Comment.Include(c => c.user).Where(c => c.ideaId == id);
            
            if (idea == null)
            {
                return NotFound();
            }
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // GET: Idea/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.User, "id", "id");
            return View();
        }

        // POST: Idea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile image, IFormFile document, [Bind("Id,Name,Content,Like_Count,Dislike_Count,File,Image,Date_Upload,CategoryId,UserId")] Idea idea)
        {

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                string documentName = Path.GetFileName(document.FileName);
                string extension = Path.GetExtension(image.FileName);
                string image_Path = Path.Combine(wwwRootPath + "/Image/", fileName + extension);
                string document_Path = Path.Combine(wwwRootPath + "/Document/", documentName);

                using (var fileStream = new FileStream(image_Path, FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }
                using (var fileStream = new FileStream(document_Path, FileMode.Create))
                {
                    await document.CopyToAsync(fileStream);
                }
                idea.File = documentName;
                idea.Image = fileName + extension;
                _context.Add(idea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }

        // GET: Idea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }

        // POST: Idea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Like_Count,Dislike_Count,File,Image,Date_Upload,CategoryId,UserId")] Idea idea)
        {
            if (id != idea.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(idea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IdeaExists(idea.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }

        // GET: Idea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .Include(i => i.Category)
                .Include(i => i.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (idea == null)
            {
                return NotFound();
            }

            return View(idea);
        }

        // POST: Idea/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var idea = await _context.Idea.FindAsync(id);
            _context.Idea.Remove(idea);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IdeaExists(int id)
        {
            return _context.Idea.Any(e => e.Id == id);
        }
        public FileResult DocumentDownload(int id)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            var idea = _context.Idea.FirstOrDefault(e => e.Id == id);
            var file = wwwRootPath + "/Document/" + idea.File;
            var fileName = idea.Name +".zip" ;
            using (var memoryStream = new MemoryStream())
            {
                using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
                {
                    zipArchive.CreateEntryFromFile(file, idea.File, CompressionLevel.Fastest);
                }
                var fileStream = new MemoryStream(memoryStream.ToArray());
                return File(fileStream, "application/zip", fileName);
            }
        }
    }
}