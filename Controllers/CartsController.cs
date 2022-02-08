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
    public class CartsController : Controller
    {
        private readonly FinalContext _context;

        public CartsController(FinalContext context)
        {
            _context = context;
        }
       
        // GET: Carts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var cart = await _context.Cart
                    .Include(c => c.user)
                    .FirstOrDefaultAsync(m => m.cartId == id);
                if (cart == null)
                {
                    return NotFound();
                }

                return View(cart);
                }

            return RedirectToAction("Index", "Home", new { area = "" });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        // GET: Carts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
            {
                return NotFound();
            }

            var cart = await _context.Cart.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            ViewData["userId"] = new SelectList(_context.User, "userId", "userId", cart.userId);
            return View(cart);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Carts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("cartId,userId")] Cart cart)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id != cart.cartId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(cart);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!CartExists(cart.cartId))
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
                ViewData["userId"] = new SelectList(_context.User, "userId", "userId", cart.userId);
                return View(cart);
            }

            return RedirectToAction("Index", "Home", new { area = "" });

        }       


        private bool CartExists(int id)
        {
            return _context.Cart.Any(e => e.cartId == id);
        }
    }
}
