using Final.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Final.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace Final.Controllers
{
    public class HomeController : Controller
    {        


        private readonly FinalContext _context;

        private DbSet<User> users;
        private DbSet<Category> categories;
        private DbSet<Product> products;
        private DbSet<Purchase> purchases;
        private DbSet<Coupon> coupons;
        private DbSet<Cart> carts;
        private DbSet<Log> logs;

        public HomeController(FinalContext context)
        {            
            _context = context;
            _context.User.Include(u => u.cart).Load();
            users = context.User;

            _context.Product.Load();
            products = context.Product;

            _context.Category.Load();
            categories = context.Category;

            _context.Coupon.Load();
            coupons = context.Coupon;

            _context.Cart.Include(c => c.CartProducts).Load();
            carts = context.Cart;

            _context.Purchase.Include(p => p.cartPurchases).Load();
            purchases = context.Purchase;

            _context.Log.Include(l => l.user).Load();
            logs = context.Log;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "NoLayoutAtAll";
            return View();
        }

        [HttpPost,ActionName("Login")]
        public async Task<IActionResult> Login(Int64 CUITCUIL, String password)
        {            
            User user = await users
                .FirstOrDefaultAsync(u => u.CUITCUIL == CUITCUIL);

            if (user == null)
            {
                TempData["ErrorMessage"] = "El usuario o la contraseña ingresada son erroneos.";
                return RedirectToAction(nameof(Index));
            }

            if (user.password != password)
            {
                user.attemps++;
                if (user.attemps >= 3)
                {
                    user.locked = true;
                    users.Update(user);
                    _context.SaveChanges();

                    // -2 user has been locked
                    TempData["ErrorMessage"] = "Ha superado la cantidad de intentos erroneos, se ha bloqueado el usuario.";
                    return RedirectToAction(nameof(Index));
                    //return -2;
                }
                users.Update(user);
                _context.SaveChanges();
                TempData["ErrorMessage"] = "El usuario o la contraseña ingresada son erroneos.";
                return RedirectToAction(nameof(Index));
            }

            // -3: User is locked
            if (user.locked)
            {
                TempData["ErrorMessage"] = "El usuario esta bloqueado.";
                return RedirectToAction(nameof(Index));
            }     


            Console.WriteLine(string.Format("{0} {1}", CUITCUIL, password));

            log(user, 1);
            user.attemps = 0;
            _context.User.Update(user);
            _context.SaveChanges();

            HttpContext.Session.SetString("userId", user.userId.ToString());                        

            if (user.userType == "admin")
            {
                return RedirectToAction("WelcomeAdmin", user);
            }
            return RedirectToAction("Welcome",user);
        }

        public IActionResult Logout()
        {  
            HttpContext.Session.Clear();            

            return RedirectToAction("Index");
        }

        public IActionResult RegisterAs()
        {
            ViewBag.Title = "NoLayout";
            return View();
        }

        public IActionResult Register()
        {
            ViewBag.Title = "NoLayout";
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(Int64 dni, String name, String lastName, String email, Int64 CUITCUIL, String password)
        {
            // Validate user
            bool newUser = users.Where(u => u.dni == dni || u.CUITCUIL == CUITCUIL).FirstOrDefault() != null;
            if (!newUser)
            {
                Cart cart = new Cart { };
                User user = new User(dni, name, lastName, email, CUITCUIL, password, "client", cart, 0, false);
                users.Add(user);
                carts.Add(cart);
                await _context.SaveChangesAsync();

                log(user, 5);

                TempData["SuccessMessage"] = "Usuario creado correctamente";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Ese usuario ya esta registrado.";
                return RedirectToAction(nameof(Register));
            }              
            
        }       

        public IActionResult RegisterCompany()
        {
            ViewBag.Title = "NoLayout";
            return View();
        }
        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCompany(String name, String email, Int64 CUITCUIL, String password)
        {
            // Validate user
            bool newUser = users.Where(u => u.CUITCUIL == CUITCUIL).FirstOrDefault() != null;
            if (!newUser)
            {
                Cart cart = new Cart { };
                User user = new User( name, email, CUITCUIL, password, "company", cart, 0, false);
                users.Add(user);
                carts.Add(cart);
                await _context.SaveChangesAsync();

                log(user, 5);

                TempData["SuccessMessage"] = "Usuario creado correctamente";

                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["ErrorMessage"] = "Ese usuario ya esta registrado.";
                return RedirectToAction(nameof(RegisterCompany));                
            }
        }

        public IActionResult Welcome(User user)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                ViewBag.Title = "NoLayout";
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult WelcomeAdmin(User user)
        {
            if (!String.IsNullOrEmpty(HttpContext.Session.GetString("userId")))
            {
                ViewBag.Title = "NoLayout";
                return View(user);
            }
            return RedirectToAction(nameof(Index));
        }

        private void log(User user, int eventType)
        {
            Log log = new Log(user, eventType);

            logs.Add(log);

            _context.SaveChanges();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
