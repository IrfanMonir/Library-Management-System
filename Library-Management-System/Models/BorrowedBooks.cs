using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int BorrowID { get; set; }
        public int BookID { get; set; }
        public int MemberID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
