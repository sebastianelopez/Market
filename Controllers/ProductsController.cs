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
    
    public class ProductsController : Controller
    {
        private readonly FinalContext _context;

        private DbSet<Log> logs;
        private DbSet<User> users;

        public ProductsController(FinalContext context)
        {
            _context = context;

            _context.Log.Include(l => l.user).Load();
            logs = context.Log;

            _context.User.Include(u => u.cart).Load();
            users = context.User;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                var products = from p in _context.Product.Include(p => p.category)
                           select p;

                if (!string.IsNullOrEmpty(searchString))
                {
                    products = products.Where(p => p.name.Contains(searchString) || p.category.name.Contains(searchString));
                }

                return View(await products.ToListAsync());

            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                 if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(m => m.productId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                ViewBag.Categories = GetCategories();            
                return View();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        
        [HttpGet]        
        public async Task<IActionResult> Create(String name, double price, string description, int ammount, string categoryname)
        {
                int userId = int.Parse(HttpContext.Session.GetString("userId"));

                Category category = _context.Category.Where(c => c.name == categoryname).First();

                Product newProd = new Product(name, price, description, ammount, category);
                _context.Product.Add(newProd);
                await _context.SaveChangesAsync();
                _context.SaveChanges();

                User user = _context.User.Where(u => u.userId == userId).First();  
                
                log(user, 6);
                
                return RedirectToAction(nameof(Index));
        }

        
        public async Task<IActionResult> AddToCart(int? productid)
        {
            if (String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                TempData["ErrorMessage"] = "Debe estar logueado para realizar esa accion.";
            }

            int userId = int.Parse(HttpContext.Session.GetString("userId"));
            
            User user = await _context.User
                .FirstOrDefaultAsync(u => u.userId == userId);
            Cart newCart= await _context.Cart
                .FirstOrDefaultAsync(c => c.cartId == user.cart.cartId);
            Product product = await _context.Product
                .FirstOrDefaultAsync(p => p.productId == productid);

            CartProduct cartProduct = await _context.CartProduct
                .FirstOrDefaultAsync(cp => cp.productId == product.productId); 

            if (cartProduct != null)
            {
                cartProduct.ammount += 1;
                if (cartProduct.ammount > product.ammount)
                {
                    cartProduct.ammount -= 1;                    
                }
                users.Update(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
                newCart.products.Add(product);
                users.Update(user);
                await _context.SaveChangesAsync();

                newCart.CartProducts.Last<CartProduct>().ammount = 1;
                users.Update(user);
                await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {

                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Product.FindAsync(id);
                if (product == null)
                {
                    return NotFound();
                }
                return View(product);
            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("productId,name,price,description,ammount")] Product product)
        {
            if (id != product.productId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.productId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {               
            
                if (id == null)
                {
                    return NotFound();
                }

                var product = await _context.Product
                    .FirstOrDefaultAsync(m => m.productId == id);
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UserView(int? id,string searchString)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {

                ViewBag.Title = "NoLayout";            

                var products = from p in _context.Product.Include(p => p.category)
                               select p;

                User user = await users.FirstOrDefaultAsync(u => u.userId == id); ;

                if (!string.IsNullOrEmpty(searchString))
                {
                    products = products.Where(p => p.name.Contains(searchString) || p.category.name.Contains(searchString));
                }

                ViewBag.products = await products.ToListAsync();
                return View(user);

            }

            return RedirectToAction("Index", "Home", new { area = "" });
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.productId == id);
        }

        private List<Category> GetCategories()
        {

            List<Category> categories = _context.Category.ToList();

            return categories;
        }        

        private void log(User user, int eventType)
        {
            Log log = new Log(user, eventType);

            logs.Add(log);

            _context.SaveChanges();
        }
    }
}
