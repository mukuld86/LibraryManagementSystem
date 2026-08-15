using Microsoft.EntityFrameworkCore;
using LibraryManagementSystem.DataAccess.Models;

namespace LibraryManagementSystem.DataAccess
{
    public class LibraryDbContext : DbContext
    {
        public LibraryDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Borrowing> Borrowings { get; set; }
        public DbSet<Member> Members { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=MUKUL-PC\\SQLEXPRESS;Database=LibraryManagementSystem;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // category -> books
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            // book -> borrowings
            modelBuilder.Entity<Borrowing>()
                .HasOne(br => br.Book)
                .WithMany(b => b.Borrowings)
                .HasForeignKey(br => br.BookId)
                .OnDelete(DeleteBehavior.Restrict);
            // member -> borrowings
            modelBuilder.Entity<Borrowing>()
                .HasOne(br => br.Member)
                .WithMany(m => m.Borrowings)
                .HasForeignKey(br => br.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
