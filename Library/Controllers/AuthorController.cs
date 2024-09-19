using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Library.App_Data;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Library.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext _context;
        public AuthorController(LibraryDbContext db) { 
            _context = db;
        }
        public IActionResult Index()
        {
            List<Author> authorList = _context.Authors.ToList();
            return View(authorList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            Author authorToSave = new Author
            {
                BirthYear = author.BirthYear,
                FirstName = author.FirstName,
                LastName = author.LastName,
                BookAuthors = []
            };

            _context.Authors.Add(authorToSave);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        //трябва да направя edit и update
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var authorId = _context.Authors.Where( i => i.Id == id).FirstOrDefault();
            return View(authorId);
        }

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            _context.Authors.Update(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var authorId = _context.Authors.Where(authorId => authorId.Id == id).FirstOrDefault();
            return View(authorId);
        }
        [HttpPost]
        public IActionResult Delete(Author author)
        {
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
