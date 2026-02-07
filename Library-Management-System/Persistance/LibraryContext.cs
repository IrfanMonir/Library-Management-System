using Library_Management_System.Models;

using Microsoft.EntityFrameworkCore;

namespace Library_Management_System.Persistance
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }

        public DbSet<Books> Books { get; set; }
        public DbSet<Members> Members { get; set; }
        public DbSet<BorrowedBooks> BorrowedBooks { get; set; }
    }

}
