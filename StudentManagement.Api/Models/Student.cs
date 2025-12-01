using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagement.Api.Data
{
    [Table("Student")]
    public class Student
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public Nullable<DateTime> UpdatedAt { get; set; } = null;
    }
}