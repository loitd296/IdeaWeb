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
    public class RatingController : Controller
    {
        private readonly IdeaWebContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;


        public RatingController(IdeaWebContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        // GET: Rating
        public async Task<IActionResult> Index()
        {
            var ideaWebContext = _context.Rating.Include(r => r.Idea).Include(r => r.user);
            return View(await ideaWebContext.ToListAsync());
        }

        // GET: Rating/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Idea)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // GET: Rating/Create
        public IActionResult Create()
        {
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id");
            ViewData["userId"] = new SelectList(_context.User, "id", "id");
            return View();
        }

        // POST: Rating/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Dislike,like,IdeaId,userId")] Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rating);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", rating.IdeaId);
            ViewData["userId"] = new SelectList(_context.User, "id", "id", rating.userId);
            return View(rating);
        }
        public async Task<IActionResult> AddLike(int id)
        {
            //get session id of the user
            var userId = HttpContext.Session.GetInt32("_ID").GetValueOrDefault();
            //var userId = 2;
            if (userId == 0)
            {
                return RedirectToAction("Login", "User");
            }

            var post = _context.Idea.Include(p => p.Ratings).FirstOrDefault(p => p.Id == id);
            int IdIdea = post.Id;
            var rateExists = _context.Rating.Where(p => p.IdeaId == IdIdea && p.userId == userId && p.like == 1);
            var rateDisLikeExists = _context.Rating.Where(p => p.IdeaId == IdIdea && p.userId == userId && p.Dislike == 1);
            
            if (rateExists.FirstOrDefault() != null)
            {
                post.Like_Count--;
                _context.Update(post);
                _context.Rating.Remove(rateExists.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else if (rateDisLikeExists.FirstOrDefault() != null)
            {
                var rate = rateDisLikeExists.FirstOrDefault();
                rate.like = 1;
                rate.Dislike = 0;
                post.Like_Count++;
                post.Dislike_Count--;

                _context.Update(post);
                _context.Update(rate);
                await _context.SaveChangesAsync();
            }
            else
            {
                var rating = new Rating();
                rating.like = 1;
                rating.Dislike = 0;
                rating.IdeaId = IdIdea;
                rating.userId = userId;
                post.Like_Count++;

                _context.Add(rating);
                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("UserViewIdea", "Idea",new { id = id });
        }

        public async Task<IActionResult> AddDislike(int id)
        {            
            //get session id of the user
            var userId = HttpContext.Session.GetInt32("_ID").GetValueOrDefault();
            if (userId == 0)
            {
                return RedirectToAction("Login", "User");
            }

            var post = _context.Idea.Include(p => p.Ratings).FirstOrDefault(p => p.Id == id);
            int IdIdea = post.Id;
            var rateExists = _context.Rating.Where(p => p.IdeaId == IdIdea && p.userId == userId && p.like == 1);
            var rateDisLikeExists = _context.Rating.Where(p => p.IdeaId == IdIdea && p.userId == userId && p.Dislike == 1);
            
            if (rateDisLikeExists.FirstOrDefault() != null)
            {
                post.Dislike_Count--;
                _context.Update(post);
                _context.Rating.Remove(rateDisLikeExists.FirstOrDefault());
                await _context.SaveChangesAsync();
            }
            else if (rateExists.FirstOrDefault() != null)
            {
                var rate = rateExists.FirstOrDefault();
                rate.like = 0;
                rate.Dislike = 1;
                post.Like_Count--;
                post.Dislike_Count++;
                
                _context.Update(post);
                _context.Update(rate);
                await _context.SaveChangesAsync();
            }
            else
            {
                var rating = new Rating();
                rating.like = 0;
                rating.Dislike = 1;
                rating.IdeaId = IdIdea;
                rating.userId = userId;
                post.Dislike_Count++;

                _context.Add(rating);
                _context.Update(post);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("UserViewIdea", "Idea",new { id = id });
        }

        // GET: Rating/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating.FindAsync(id);
            if (rating == null)
            {
                return NotFound();
            }
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", rating.IdeaId);
            ViewData["userId"] = new SelectList(_context.User, "id", "id", rating.userId);
            return View(rating);
        }

        // POST: Rating/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Dislike,like,IdeaId,userId")] Rating rating)
        {
            if (id != rating.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rating);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RatingExists(rating.Id))
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
            ViewData["IdeaId"] = new SelectList(_context.Idea, "Id", "Id", rating.IdeaId);
            ViewData["userId"] = new SelectList(_context.User, "id", "id", rating.userId);
            return View(rating);
        }

        // GET: Rating/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rating = await _context.Rating
                .Include(r => r.Idea)
                .Include(r => r.user)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rating == null)
            {
                return NotFound();
            }

            return View(rating);
        }

        // POST: Rating/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rating = await _context.Rating.FindAsync(id);
            _context.Rating.Remove(rating);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RatingExists(int id)
        {
            return _context.Rating.Any(e => e.Id == id);
        }
    }
}
