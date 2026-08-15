using LibraryManagementSystem.DataAccess.Interfaces;
using LibraryManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class BorrowingRepository : IBorrowingRepository
    {
        private readonly LibraryDbContext _context;
        public BorrowingRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void AddBorrowing(Borrowing borrowing)
        {
            _context.Borrowings.Add(borrowing);
            _context.SaveChanges();
        }
        public List<Borrowing> GetAllBorrowings()
        {
            _context.Borrowings
                .Include(b=>b.Book)
                .Include(b=>b.Member)
                .ToList();
        }
        public Borrowing GetBorrowing(int borrowingId)
        {
            return _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Member)
                .FirstOrDefault(b => b.BorrowingId == borrowingId);
        }
        public void UpdateBorrowing(Borrowing borrowing)
        {
            _context.Borrowings.Update(borrowing);
            _context.SaveChanges();
        }
        public List<Borrowing> GetActiveBorrowings()
        {
            return _context.Borrowings
                .Include(b => b.Book)
                .Include(b => b.Member)
                .Where(b => b.ReturnDate == null)
                .ToList();
        }
    }
}
