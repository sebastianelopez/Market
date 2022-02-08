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
    public class LogsController : Controller
    {
        private readonly FinalContext _context;

        public LogsController(FinalContext context)
        {            
                _context = context;
        }

        // GET: Logs
        public async Task<IActionResult> Index()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                return View(await _context.Log.Include(l => l.user).ToListAsync());
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }        


        // GET: Logs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var log = await _context.Log
                    .FirstOrDefaultAsync(m => m.logId == id);
                if (log == null)
                {
                    return NotFound();
                }

                return View(log);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Logs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var log = await _context.Log.FindAsync(id);
            _context.Log.Remove(log);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Log eliminado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        private bool LogExists(int id)
        {
            return _context.Log.Any(e => e.logId == id);
        }
    }
}
