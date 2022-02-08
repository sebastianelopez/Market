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
    public class UsersController : Controller
    {
        private readonly FinalContext _context;

        public UsersController(FinalContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View(await _context.User.ToListAsync());
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(u => u.userId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.User.FindAsync(id);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId,dni,name,lastName,email,CUITCUIL,password,userType,attemps,locked")] User user)
        {
            if (id != user.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.userId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var user = await _context.User
                    .FirstOrDefaultAsync(m => m.userId == id);
                if (user == null)
                {
                    return NotFound();
                }

                return View(user);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.userId == id);
        }
    }
}
