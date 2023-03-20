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
    public class CommentController : Controller
    {
        private IWebHostEnvironment _hostEnvironment;
        private readonly IdeaWebContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CommentController(IdeaWebContext context, IWebHostEnvironment hostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }
        // GET: Comment
        public async Task<IActionResult> Index()
        {
            var ideaWebContext = _context.Comment.Include(c => c.idea).Include(c => c.user);
            return View(await ideaWebContext.ToListAsync());
        }

        // GET: Comment/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.idea)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comment/Create
        public IActionResult Create()
        {
            ViewData["ideaId"] = new SelectList(_context.Idea, "Id", "Id");
            ViewData["userId"] = new SelectList(_context.User, "id", "id");
            return View();
        }

        // POST: Comment/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content")] Comment comment, int ideaId, int status)
        {
            var userId = HttpContext.Session.GetInt32("_ID").GetValueOrDefault();
            if (userId == 0)
            {
                return RedirectToAction("Login", "User");
            }
            Console.WriteLine(ModelState.IsValid);
            if (ModelState.IsValid)
            {
                comment.ideaId = ideaId;
                comment.Date_Upload = DateTime.Now;
                comment.Status = status;
                comment.userId = userId;
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction("UserViewIdea", "Idea", new { id = ideaId });
            }
            return View();
        }

        // GET: Comment/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            ViewData["ideaId"] = new SelectList(_context.Idea, "Id", "Id", comment.ideaId);
            ViewData["userId"] = new SelectList(_context.User, "id", "id", comment.userId);
            return View(comment);
        }

        // POST: Comment/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date_Upload,Content,Status,ideaId,userId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            ViewData["ideaId"] = new SelectList(_context.Idea, "Id", "Id", comment.ideaId);
            ViewData["userId"] = new SelectList(_context.User, "id", "id", comment.userId);
            return View(comment);
        }

        // GET: Comment/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .Include(c => c.idea)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }
        public async Task<IActionResult> UserDelete(int? id)
        {
            var comment = await _context.Comment
                .Include(c => c.idea)
                .Include(c => c.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction("UserViewIdea", "Idea", new { id = comment.idea.Id }); ;
        }

        // POST: Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
