using Library.App_Data;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using BCrypt.Net;
using Library.Models.ViewModel;
using Newtonsoft.Json;

namespace Library.Controllers
{
    public class HomeController : Controller

    {
        private readonly LibraryDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, LibraryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (_context.Users.Any(u => u.Email == user.Email))
            {
                ModelState.AddModelError("reg", "This account already exists.");
                return View(user);
            }
            User newUser = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = HashPassword(user.Password)
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return View();
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public IActionResult LogIn(LoginVM model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            User user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
            if (user == null)
            {
                this.ModelState.AddModelError("AuthError", "Invalid data");
                return View(model);
            }

            if (!VerifyPassword(model.Password, user.Password))
            {
                this.ModelState.AddModelError("AuthError", "Invalid data!!!");
                return View(model);
            }

            string jsonData = JsonConvert.SerializeObject(user);
            HttpContext.Session.SetString("loggedUser", jsonData);

            return RedirectToAction("Index");
        }

        
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string inputPassword, string storedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(inputPassword, storedPassword);
        }
    }
}
