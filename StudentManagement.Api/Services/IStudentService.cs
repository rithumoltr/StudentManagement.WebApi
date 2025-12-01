using StudentManagement.Api.Common;
using StudentManagement.Api.Data;
using StudentManagement.Api.DTO;

namespace StudentManagement.Api.Services
{
    public interface IStudentService
    {
        Task<ApiResponse<Student>> AddStudent(CreateStudentDto studentDto,CancellationToken cancellationToken);
        Task<ApiResponse<List<ReadStudentDto>>> GetAllStudentsAsync(CancellationToken cancellationToken);
        Task<ApiResponse<ReadStudentDto>> GetStudentByIdAsync(int id, CancellationToken cancellationToken);
        Task<ApiResponse<object>> UpdateStudentById(int id, CreateStudentDto studentDto, CancellationToken cancellationToken);
        Task<ApiResponse<object>> DeleteStudentById(int id, CancellationToken cancellationToken);
    }
}
