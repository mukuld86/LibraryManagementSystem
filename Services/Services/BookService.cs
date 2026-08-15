using LibraryManagementSystem.DataAccess.Interfaces;
using LibraryManagementSystem.DataAccess.Models;

using LibraryManagementSystem.Services.Interfaces;

namespace LibraryManagementSystem.Services.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public void AddBook(Book book)
        {
            _bookRepository.AddBook(book);
        }

        public void DeleteBook(int bookId)
        {
            _bookRepository.DeleteBook(bookId);
        }

        public List<Book> GetAllBooks()
        {
            return _bookRepository.GetAllBooks();
        }

        public List<Book> GetAvailableBooks()
        {
            return _bookRepository.GetAvailableBooks();
        }

        public Book GetBook(int bookId)
        {
            return _bookRepository.GetBook(bookId);
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return _bookRepository.SearchBooks(searchTerm);
        }

        public void UpdateBook(Book book)
        {
            _bookRepository.UpdateBook(book);
        }
    }
}
