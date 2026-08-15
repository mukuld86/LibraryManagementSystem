using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.Services.Interfaces
{
    public interface ICategoryService
    {
        void AddCategory(Category category);
        List<Category> GetAllCategories();
        Category GetCategory(int categoryId);
        void UpdateCategory(Category category);
        void DeleteCategory(int categoryId);
    }
}
