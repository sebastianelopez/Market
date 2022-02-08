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
    public class CouponsController : Controller
    {
        private readonly FinalContext _context;

        public CouponsController(FinalContext context)
        {
            _context = context;
        }

        // GET: Coupons
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View(await _context.Coupon.ToListAsync());
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Coupons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var coupon = await _context.Coupon
                    .FirstOrDefaultAsync(m => m.id == id);
                if (coupon == null)
                {
                    return NotFound();
                }

                return View(coupon);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Coupons/Create
        public IActionResult Create()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View();
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Coupons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,code,percentage")] Coupon coupon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(coupon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(coupon);
        }

        // GET: Coupons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var coupon = await _context.Coupon.FindAsync(id);
                if (coupon == null)
                {
                    return NotFound();
                }
                return View(coupon);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Coupons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,code,percentage")] Coupon coupon)
        {
            if (id != coupon.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(coupon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CouponExists(coupon.id))
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
            return View(coupon);
        }

        // GET: Coupons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var coupon = await _context.Coupon
                    .FirstOrDefaultAsync(m => m.id == id);
                if (coupon == null)
                {
                    return NotFound();
                }

                return View(coupon);
                }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Coupons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var coupon = await _context.Coupon.FindAsync(id);
            _context.Coupon.Remove(coupon);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CouponExists(int id)
        {
            return _context.Coupon.Any(e => e.id == id);
        }
    }
}
