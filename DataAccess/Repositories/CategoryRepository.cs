using LibraryManagementSystem.DataAccess.Interfaces;
using LibraryManagementSystem.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly LibraryDbContext _context;
        public CategoryRepository(LibraryDbContext context)
        {
            _context = context;
        }
        public void AddCategory(Category category)
        {

            _context.Categories.Add(category);
            _context.SaveChanges();
        }
        public List<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }
        public Category GetCategory(int categoryId)
        {
            return _context.Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }
        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
        }
        public void DeleteCategory(int categoryId)
        {
            var category = GetCategory(categoryId);
            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }
    }
}
