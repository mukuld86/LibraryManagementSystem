
using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DataAccess.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required!")]
        [StringLength(50)]
        public string CategoryName { get; set; }
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
