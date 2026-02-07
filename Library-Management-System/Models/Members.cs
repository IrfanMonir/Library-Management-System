using System.ComponentModel.DataAnnotations;

namespace Library_Management_System.Models
{
    public class Members
    {
        [Key]
        public int MemberID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
    }
}
