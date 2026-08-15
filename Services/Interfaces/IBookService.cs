
using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IBookService
    {
        void AddBook(Book book);
        List<Book> GetAllBooks();
        Book GetBook(int bookId);
        void UpdateBook(Book book);
        void DeleteBook(int bookId);
        List<Book> SearchBooks(string searchTerm);
        List<Book> GetAvailableBooks();

    }
}
