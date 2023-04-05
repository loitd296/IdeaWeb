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
    public class CategoryController : Controller
    {

        private readonly IdeaWebContext _context;
        private readonly IdeaWebContext _secondContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryController(IdeaWebContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _secondContext = context;
        }

        // GET: Category
        public async Task<IActionResult> Index(int pg = 1)
        {
            ViewBag.Layout = "indexAdmin";

            const int pageSize = 5;
            if (pg < 1)
                pg = 1;
            int recsCount = _context.Category.Count();
            var pager = new Pager(recsCount, pg, pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = _context.Category.Skip(recSkip).Take(pager.PageSize).ToList();
            this.ViewBag.Pager = pager;
            return View(data);
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
            var results = _context.Category.Skip(recSkip).Take(pager.PageSize).Where(d => d.Name.Contains(query)).ToList();

            // Pass the results to the view
            if (results.Count() == 0)
            {
                return RedirectToAction("Index");
            }
            return View(results);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Category/Create
        public IActionResult Create()
        {
            ViewBag.Layout = "indexAdmin";
            return View();
        }

        // POST: Category/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Status,Deleted_Status")] Category category)
        {
            ViewBag.Layout = "indexAdmin";
            var cat = _context.Category.Where(i => i.Name == category.Name).ToList();
            if (ModelState.IsValid && cat.Count() <= 0)
            {
                category.Status = 1;
                category.Deleted_Status = 0;
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else if (cat.Count() > 0)
            {
                ViewBag.ErrorMessage = "Category is exist!";
            }
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Category/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool CancleDeleteCheckbox, [Bind("Id,Name,Status,Deleted_Status")] Category category1)
        {
            ViewBag.Layout = "indexAdmin";
            var category = _secondContext.Category.FirstOrDefault(i => i.Id == category1.Id);
            var cat = _context.Category.Where(i => i.Name == category.Name).ToList();
            
            //var check = cat.Any(n => n.Equals(category.Name));
            if (id != category1.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (CancleDeleteCheckbox == true)
                    {
                        category.Status = 1;
                        category.Deleted_Status = 0;
                        _context.Update(category);
                        await _context.SaveChangesAsync();
                    }
                    else if (CancleDeleteCheckbox == false)
                    {
                        _context.Update(category);
                        await _context.SaveChangesAsync();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category1.Id))
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
            else if (category1.Name != null)
            {
                if (cat.Count() >= 1  && category.Name != category1.Name)
                {
                    ViewBag.ErrorMessage = "Category is exist!";
                }
            }
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Layout = "indexAdmin";
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Layout = "indexAdmin";
            var category = await _context.Category.FindAsync(id);
            var IdeaHaveCategory = _context.Idea.Where(i => i.CategoryId == id).ToList();
            if (IdeaHaveCategory.Count() <= 0)
            {
                _context.Category.Remove(category);
            }
            else if (IdeaHaveCategory.Count() > 0)
            {
                category.Status = 0;
                category.Deleted_Status = 1;
                _context.Update(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }

    }
}
