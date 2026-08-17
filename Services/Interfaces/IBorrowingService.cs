using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface IBorrowingService
    {
        bool BorrowBook(int bookId, int memberId);
        bool ReturnBook(int borrowingId);
        List<Borrowing> GetAllBorrowings();
        List<Borrowing> GetActiveBorrowings();
    }
}
