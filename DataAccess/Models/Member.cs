using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DataAccess.Models
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        [Required]
        [StringLength(100)]
        public string MemberName { get; set; } 
        [Required]
        [EmailAddress]
        public string Email { get; set; } 
        [Required]
        [StringLength(15)]
        public string PhoneNumber { get; set; } 
        public ICollection<Borrowing> Borrowings { get; set; } = new List<Borrowing>();
    }
}
