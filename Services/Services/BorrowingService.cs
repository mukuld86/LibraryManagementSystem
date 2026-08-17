using LibraryManagementSystem.DataAccess.Models;
using LibraryManagementSystem.DataAccess.Interfaces;
using LibraryManagementSystem.Services.Interfaces;

namespace LibraryManagementSystem.Services.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IBorrowingRepository _borrowingRepository;
        private readonly IBookRepository _bookRepository;
        private readonly IMemberRepository _memberRepository;

        public BorrowingService(IBorrowingRepository borrowingRepository, IBookRepository bookRepository, IMemberRepository memberRepository)
        {
            _borrowingRepository = borrowingRepository;
            _bookRepository = bookRepository;
            _memberRepository = memberRepository;
        }

        public bool BorrowBook(int bookId, int memberId)
        {
            var book = _bookRepository.GetBook(bookId);
            if (book == null)
            {
                return false;
            }
            if (!book.IsAvailable)
            {
                return false;
            }
            var member = _memberRepository.GetMember(memberId);
            if (member == null)
            {
                return false;
            }
            var borrowing = new Borrowing
            {
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.Now,
                ReturnDate=null
            };
            _borrowingRepository.AddBorrowing(borrowing);
            book.IsAvailable = false;
            _bookRepository.UpdateBook(book);
            return true;
        }

        public bool ReturnBook(int borrowingId)
        {
            var borrowing = _borrowingRepository.GetBorrowing(borrowingId);
            if (borrowing == null)
            {
                return false;
            }
            if (borrowing.ReturnDate != null)
            {
                return false;
            }
            borrowing.ReturnDate = DateTime.Now;
            _borrowingRepository.UpdateBorrowing(borrowing);
            var book = _bookRepository.GetBook(borrowing.BookId);
            if (book != null)
            {
                book.IsAvailable = true;
                _bookRepository.UpdateBook(book);
            }
            return true;
        }

        public List<Borrowing> GetAllBorrowings()
        {
            return _borrowingRepository.GetAllBorrowings();
        }

        public List<Borrowing> GetActiveBorrowings()
        {
            return _borrowingRepository.GetActiveBorrowings();
        }
    }
}
