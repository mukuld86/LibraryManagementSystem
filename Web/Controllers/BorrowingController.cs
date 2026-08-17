using LibraryManagementSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagementSystem.Web.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IBookService _bookService;
        private readonly IBorrowingService _borrowingService;
        public BorrowingController(IMemberService memberService, IBookService bookService, IBorrowingService borrowingService)
        {
            _memberService = memberService;
            _bookService = bookService;
            _borrowingService = borrowingService;
        }

        public IActionResult Index()
        {
            var borrowings = _borrowingService.GetAllBorrowings();
            return View(borrowings);
        }
        [HttpGet]
        public IActionResult Borrow()
        {
            LoadBooksAndMembers();
            return View();
        }
        [HttpPost]
        public IActionResult Borrow(int bookId, int memberId)
        {
            bool success = _borrowingService.BorrowBook(bookId, memberId);
            if (!success)
            {
                ModelState.AddModelError("",
                    "The book cannot be borrowed. Please check if the book is available and the member exists!");
                LoadBooksAndMembers();
                return View();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Return(int borrowingId)
        {
            bool success = _borrowingService.ReturnBook(borrowingId);
            if (!success)
            {
                return NotFound("Borrowing record not found or the book has already been returned!");
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Active()
        {
            var borrowings = _borrowingService.GetActiveBorrowings();
            return View("Index", borrowings);
        }
        private void LoadBooksAndMembers()
        {
            var books = _bookService.GetAllBooks();
            var members = _memberService.GetAllMembers();
            ViewBag.Books = new SelectList(
                books,
                "BookId",
                "Title");
            ViewBag.Members = new SelectList(
                members,
                "MemberId",
                "MemberName");
        }
    }
}
