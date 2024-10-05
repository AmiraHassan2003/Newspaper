using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NewsPaper.Models.Context;
using NewsPaper.Models.Entities;
using NewsPaper.Repository.Author;

namespace NewsPaper.Controllers
{
    public class AuthorController : Controller
    {
        IAuthorRepository authorRepo;
        NewspaperContext context;
        public AuthorController(IAuthorRepository authorRepository, NewspaperContext context)
        {
            authorRepo = authorRepository;
            this.context = context;
        }

        public IActionResult GetAll()
        {
            ViewBag.authors = authorRepo.GetAll();
            ViewBag.departments = context.departments.Include(d=>d.authors).ToList();
            var auth = new Author();

            return View("GetAll", auth);
        }

        public IActionResult DeleteAuthor(int id) 
        {
            //Author auth = context.authors.FirstOrDefault(a => a.Id == id);
            authorRepo.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult UpdateAuthor(int id)
        {
            var auth = authorRepo.GetById(id);
            ViewBag.departments = context.departments.Include(a=>a.authors).ToList();
            return View("UpdateAuthor", auth);
        }

        [HttpPost]
        public IActionResult SaveAuthor(Author author, int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    authorRepo.Update(author, id);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            
            return RedirectToAction("GetAll");
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    authorRepo.Add(author);
                }
            }
            catch (Exception ex) 
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAuthorsById(int id)
        {
            ViewBag.authors = context.authors.Include(a => a.department).Where(a => a.departmentId == id).ToList();
            ViewBag.departments = context.departments.Include(d => d.authors).ToList();
            var auth = new Author();
            return View(auth);
        }
    }
}
