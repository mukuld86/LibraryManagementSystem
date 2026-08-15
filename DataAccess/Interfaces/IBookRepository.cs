using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.DataAccess.Interfaces
{
    internal interface IBookRepository
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
