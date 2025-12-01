using StudentManagement.Api.Common;
using StudentManagement.Api.Data;

namespace StudentManagement.Api.Repositories
{
    public interface IStudentRepository
    {
        Task AddStudentAsync(Student student);
        Task<OperationResult> SaveChangesAsync();
        Task<bool> EmailExist(string email);
        Task<List<Student>> GetAllStudents(CancellationToken token);
        Task<Student> GetStudentByIdAsync(int id, CancellationToken token);
        Task<OperationResult> DeleteStudentAsync(int id, CancellationToken token);
    }
}
