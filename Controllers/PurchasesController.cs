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
    public class PurchasesController : Controller
    {
        private readonly FinalContext _context;

        public PurchasesController(FinalContext context)
        {
            _context = context;
        }

        // GET: Purchases
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {

                return View(await _context.Purchase.ToListAsync());
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Purchases/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var purchase = await _context.Purchase
                    .FirstOrDefaultAsync(m => m.purchaseId == id);
                if (purchase == null)
                {
                    return NotFound();
                }

                return View(purchase);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }
       

        // GET: Purchases/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var purchase = await _context.Purchase.FindAsync(id);
                if (purchase == null)
                {
                    return NotFound();
                }
                return View(purchase);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Purchases/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("purchaseId,total")] Purchase purchase)
        {
            if (id != purchase.purchaseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(purchase);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PurchaseExists(purchase.purchaseId))
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
            return View(purchase);
        }

        // GET: Purchases/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var purchase = await _context.Purchase
                    .FirstOrDefaultAsync(m => m.purchaseId == id);
                if (purchase == null)
                {
                    return NotFound();
                }

                return View(purchase);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Purchases/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var purchase = await _context.Purchase.FindAsync(id);
            _context.Purchase.Remove(purchase);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PurchaseExists(int id)
        {
            return _context.Purchase.Any(e => e.purchaseId == id);
        }
    }
}
