using LibraryManagementSystem.DataAccess.Models;
namespace LibraryManagementSystem.DataAccess.Interfaces
{
    internal interface ICategoryRepository
    {
        void AddCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategory(int categoryId);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
