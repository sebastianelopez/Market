using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using Microsoft.AspNetCore.Http;

namespace Final.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly FinalContext _context;

        public CategoriesController(FinalContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View(await _context.Category.ToListAsync());
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var category = await _context.Category
                    .FirstOrDefaultAsync(m => m.categoryId == id);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("categoryId,name")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
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

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("categoryId,name")] Category category)
        {
            if (id != category.categoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.categoryId))
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
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var category = await _context.Category
                    .FirstOrDefaultAsync(m => m.categoryId == id);
                if (category == null)
                {
                    return NotFound();
                }

                return View(category);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Category.Any(e => e.categoryId == id);
        }
    }
}
