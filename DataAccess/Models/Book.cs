
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryManagementSystem.DataAccess.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; } 
        [Required]
        [StringLength(100)]
        public string Author { get; set; } 
        [Required]
        [StringLength(20)]
        public string ISBN { get; set; } 
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        
        public bool IsAvailable { get; set; } 
        public Category? Category { get; set; }
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
