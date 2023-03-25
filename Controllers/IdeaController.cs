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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IdeaWebContext _secondContext;
        public IdeaController(IdeaWebContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
            _secondContext = context;
        }

        // GET: Idea
        public async Task<IActionResult> Index(int pg = 1)
        {
            ViewBag.Layout = "indexAdmin";
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Idea.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _context.Idea.Include(i => i.CloseDateAcedamic).Include(i => i.Category).Include(i => i.User).Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            var ideaWebContext = _context.Idea.Include(i => i.Category).Include(i => i.User);
            return View(data);
        }

        // GET: Idea/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Layout = "indexAdmin";

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
            ViewBag.Layout = "indexAdmin";

            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.User, "id", "name");
            return View();
        }

        // POST: Idea/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile image, IFormFile document, [Bind("Id,Name,Content,Like_Count,Dislike_Count,File,Image,Date_Upload,CloseDateAcedamicId,CategoryId,UserId")] Idea idea)
        {
            ViewBag.Layout = "indexAdmin";

            var closeDate = await _context.CloseDateAcedamic.FindAsync(idea.CloseDateAcedamicId);
            if (idea.Date_Upload > closeDate.CloseDatePostIdea)
            {
                return RedirectToAction(nameof(ErrorMessage));
            }
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
            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }
        public IActionResult ErrorMessage()
        {
            ViewBag.AlertMsg = "The closing date for new ideas cannot exceed the final closing date!!!";
            return View();
        }
        public IActionResult ErrorMessageForUser()
        {
            ViewBag.AlertMsg = "The closing date for new ideas cannot exceed the final closing date!!!";
            return View();
        }

        // GET: Idea/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }
            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name", idea.CloseDateAcedamicId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }

        // POST: Idea/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Content,Like_Count,Dislike_Count,File,Image,Date_Upload,CloseDateAcedamicId,CategoryId,UserId")] Idea idea)
        {
            ViewBag.Layout = "indexAdmin";

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
            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name", idea.CloseDateAcedamicId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Id", idea.CategoryId);
            ViewData["UserId"] = new SelectList(_context.User, "id", "id", idea.UserId);
            return View(idea);
        }
        public async Task<IActionResult> UserEditIdea(int? id)
        {


            var idea = await _context.Idea.FindAsync(id);
            if (idea == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", idea.CategoryId);
            return View(idea);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserEditIdea(IFormFile image, IFormFile document, [Bind("Id,Name,Content,CategoryId")] Idea Editidea)
        {
            var idea = await _secondContext.Idea.FindAsync(Editidea.Id);
            string wwwRootPath = _hostEnvironment.WebRootPath;
            try
            {
                idea.Name = Editidea.Name != null ? Editidea.Name : idea.Name;
                idea.Content = Editidea.Content != null ? Editidea.Content : idea.Content;
                idea.CategoryId = Editidea.CategoryId != null ? Editidea.CategoryId : idea.CategoryId;
                if (image != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(image.FileName);
                    string extension = Path.GetExtension(image.FileName);
                    string image_Path = Path.Combine(wwwRootPath + "/Image/", fileName + extension);
                    using (var fileStream = new FileStream(image_Path, FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }
                    idea.Image = fileName + extension;
                }
                if (document != null)
                {
                    string documentName = Path.GetFileName(document.FileName);
                    string document_Path = Path.Combine(wwwRootPath + "/Document/", documentName);
                    using (var fileStream = new FileStream(document_Path, FileMode.Create))
                    {
                        await document.CopyToAsync(fileStream);
                    }
                    idea.File = documentName;

                }
                _context.Update(idea);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IdeaExists(Editidea.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("UserViewIdea", "Idea", new { id = idea.Id });
        }


        // GET: Idea/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Layout = "indexAdmin";

            if (id == null)
            {
                return NotFound();
            }

            var idea = await _context.Idea
                .Include(i => i.CloseDateAcedamic)
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

        public async Task<IActionResult> UserDelete(int? id)
        {
            var idea = await _context.Idea.FindAsync(id);
            _context.Idea.Remove(idea);
            await _context.SaveChangesAsync();
            return RedirectToAction("IdeaIndex", "Idea");
        }
        private bool IdeaExists(int id)
        {
            return _context.Idea.Any(e => e.Id == id);
        }

        public IActionResult UserCreateIdea()
        {
            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name");
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserCreateIdea(IFormFile image, IFormFile document, [Bind("Id,Name,Content,File,Image,CategoryId,CloseDateAcedamicId")] Idea idea)
        {
            var closeDate = await _context.CloseDateAcedamic.FindAsync(idea.CloseDateAcedamicId);
            if (DateTime.Now > closeDate.CloseDatePostIdea)
            {
                return RedirectToAction(nameof(ErrorMessageForUser));
            }
            var userId = HttpContext.Session.GetInt32("_ID").GetValueOrDefault();
            if (userId == 0)
            {
                return RedirectToAction("Login", "User");
            }
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
                idea.Like_Count = 0;
                idea.Dislike_Count = 0;
                idea.Date_Upload = DateTime.Now;
                idea.UserId = userId;
                idea.File = documentName;
                idea.Image = fileName + extension;
                _context.Add(idea);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CloseDateAcedamicId"] = new SelectList(_context.CloseDateAcedamic, "Id", "Name", idea.CloseDateAcedamicId);
            ViewData["CategoryId"] = new SelectList(_context.Category, "Id", "Name", idea.CategoryId);
            return View(idea);
        }
        public async Task<IActionResult> UserViewIdea(int id)
        {
            var idea = await _context.Idea
            .Include(i => i.Category)
            .Include(i => i.Comments)
            .Include(i => i.User)
            .ThenInclude(i => i.Department)
            .FirstOrDefaultAsync(m => m.Id == id);
            ViewBag.comment = _context.Comment.Include(c => c.user).Where(c => c.ideaId == id).OrderByDescending(p => p.Date_Upload);
            ViewBag.commentCount = _context.Comment.Where(c => c.ideaId == id).Count();
            ViewBag.id = id;
            ViewBag.UserId = HttpContext.Session.GetInt32("_ID").GetValueOrDefault();
            return View(idea);
        }
        public async Task<IActionResult> IdeaIndex(int pg = 1)
        {
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Category.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _context.Idea.Include(i => i.Category).Include(i => i.User).OrderByDescending(p => p.Date_Upload).ToList();
            this.ViewBag.Pager = pager;
            ViewBag.commentCount = _context.Comment.ToList();
            return View(data);
        }

        public FileResult DocumentDownload(int id)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;
            var idea = _context.Idea.FirstOrDefault(e => e.Id == id);
            var file = wwwRootPath + "/Document/" + idea.File;
            var fileName = idea.Name + ".zip";
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
        [HttpGet]
        public ActionResult Search(string query, int pg = 1)
        {
            ViewBag.Layout = "indexAdmin";
            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Idea.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            this.ViewBag.Pager = pager;
            // Query the data source using Entity Framework
            var results = _context.Idea.Include(i => i.CloseDateAcedamic).Include(i => i.Category).Include(i => i.User).Skip(recSkip).Take(pager.PageSize).Where(d => d.Name.Contains(query)).ToList();

            // Pass the results to the view
            return View(results);
        }
        public ActionResult SearchforUser(string query, int pg = 1)
        {

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Idea.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            this.ViewBag.Pager = pager;
            // Query the data source using Entity Framework
            ViewBag.commentCount = _context.Comment.ToList();
            var results = _context.Idea.Include(i => i.Category).Include(i => i.User).OrderByDescending(p => p.Date_Upload).Skip(recSkip).Take(pager.PageSize).Where(d => d.Name.Contains(query)).ToList();

            // Pass the results to the view
            return View(results);
        }
    }
}