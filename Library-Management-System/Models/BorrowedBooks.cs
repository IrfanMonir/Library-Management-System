using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library_Management_System.Models
{
    public class BorrowedBooks
    {
        [Key]
        public int BorrowID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int BookID { get; set; }
        [ForeignKey("BookID")]
        public Books Books { get; set; }   // Navigation property

        public int MemberID { get; set; }
        [ForeignKey("MemberID")]
        public Members Members { get; set; }
    }
}
