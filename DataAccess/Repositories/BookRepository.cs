using LibraryManagementSystem.DataAccess.Interfaces;
using LibraryManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly LibraryDbContext _context;
        public BookRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }
        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }
        public Book GetBook(int bookId)
        {
            return _context.Books
                .Include(b=>b.Category)
                .FirstOrDefault(b => b.BookId == bookId);
        }
        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }
        public void DeleteBook(int bookId)
        {
            var book = GetBook(bookId);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges();
            }
        }
        public List<Book> SearchBooks(string searchTerm)
        {
            return _context.Books
                .Include(b => b.Category)
                .Where(b =>
                b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm))
                .ToList();
        }
        public List<Book> GetAvailableBooks()
        {
            return _context.Books
                .Include(b => b.Category)
                .Where(b => b.IsAvailable)
                .ToList();
        }
    }
}
