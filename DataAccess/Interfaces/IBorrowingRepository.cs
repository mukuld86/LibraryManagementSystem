using LibraryManagementSystem.DataAccess.Models;
namespace LibraryManagementSystem.DataAccess.Interfaces
{
    public interface IBorrowingRepository
    {
        void AddBorrowing(Borrowing borrowing);
        List<Borrowing> GetAllBorrowings();
        Borrowing GetBorrowing(int borrowingId);
        void UpdateBorrowing(Borrowing borrowing);
        List<Borrowing> GetActiveBorrowings();
    }
}
