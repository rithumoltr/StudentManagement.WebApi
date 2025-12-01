using System.ComponentModel.DataAnnotations;

namespace StudentManagement.Api.Data
{
    public class Enrollments
    {
        [Key]
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public string Grade { get; set; }
        public DateTime EnrolledOn { get; set; }
        public DateTime CreatedAt { get; set; }
        public Nullable<DateTime> UpdatedAt { get; set; }
    }
}
