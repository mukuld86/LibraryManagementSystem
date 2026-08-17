using LibraryManagementSystem.DataAccess.Models;
using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Web.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICategoryService _categoryService;
        public BookController(IBookService bookService, ICategoryService categoryService)
        {
            _bookService = bookService;
            _categoryService = categoryService;
        }

        public ActionResult Index()
        {
            var books = _bookService.GetAllBooks();
            return View(books);
        }
        [HttpGet]
        public ActionResult Add()
        {
            LoadCategories();
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(book);
            }
            book.IsAvailable = true;
            _bookService.AddBook(book);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            LoadCategories();
            return View(book);
        }
        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View(book);
            }
            _bookService.UpdateBook(book);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var book = _bookService.GetBook(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _bookService.DeleteBook(id);
                return RedirectToAction(nameof(Index));
            }
            catch(Exception)
            {
                return View("Error");
            }

        }
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return RedirectToAction(nameof(Index));
            }
            var books = _bookService.SearchBooks(searchTerm);
            return View("Index",books);
        }
        private void LoadCategories()
        {
            var categories = _categoryService.GetAllCategories();
            ViewBag.Categories = new SelectList(
                categories,
                "CategoryId",
                "CategoryName");
        }
    }
}
