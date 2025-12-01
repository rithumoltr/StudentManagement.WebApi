namespace StudentManagement.Api.DTO
{
    public class ReadStudentDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public char Gender { get; set; }
    }
}
