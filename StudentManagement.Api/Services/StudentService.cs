using StudentManagement.Api.DTO;
using StudentManagement.Api.Data;
using StudentManagement.Api.Repositories;
using StudentManagement.Api.Common;
using Microsoft.Identity.Client;

namespace StudentManagement.Api.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studRepository;

        public StudentService(IStudentRepository studRepository)
        {
            _studRepository = studRepository;
        }

        public async Task<ApiResponse<Student>> AddStudent(CreateStudentDto studentDto,CancellationToken cancellationToken)
        {
            bool emailExist = await _studRepository.EmailExist(studentDto.Email);
            if (emailExist)
            {
                throw new ConflictException("Email already exists");
                //return new ApiResponse<Student>
                //{
                //    IsSuccess = false,
                //    Message = "Email already exists",
                //};
            }
            var student = new Student { 
                FirstName = studentDto.FirstName,
                LastName = studentDto.LastName,
                Email = studentDto.Email,
                DateOfBirth = studentDto.DateOfBirth,
                Gender = studentDto.Gender,
            };
            await _studRepository.AddStudentAsync(student);
            var result = await _studRepository.SaveChangesAsync();
            if (result.IsSuccess)
            {
                return new ApiResponse<Student>
                {
                    IsSuccess = true,
                    Message = "Student added successfully",
                };
            }
            else
            {
                return new ApiResponse<Student>
                {
                    IsSuccess = false,
                    Message = "Student added failed",
                };
            }
        }
        public async Task<ApiResponse<List<ReadStudentDto>>> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            List<ReadStudentDto> studentDtos = new List<ReadStudentDto>();
            var students = await _studRepository.GetAllStudents(cancellationToken);
            if(students == null || students.Count == 0)
            {
                return new ApiResponse<List<ReadStudentDto>>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "No students found",
                };
            }
            foreach(var student in students)
            {
                studentDtos.Add(new ReadStudentDto
                {
                    Name = $"{student.FirstName} {student.LastName}",
                    DateOfBirth = student.DateOfBirth,
                    Email = student.Email,
                    Gender = student.Gender,
                });
            }
            return new ApiResponse<List<ReadStudentDto>>
            {
                IsSuccess = true,
                Data = studentDtos,
                Message = "Students retrieved successfully",
            };
        }
        public async Task<ApiResponse<ReadStudentDto>> GetStudentByIdAsync(int id, CancellationToken cancellationToken)
        {
            var student = await _studRepository.GetStudentByIdAsync(id, cancellationToken);
            if (student == null)
            {
                return new ApiResponse<ReadStudentDto>
                {
                    IsSuccess = false,
                    Data = null,
                    Message = "Student not found",
                };
            }
            var studentDto = new ReadStudentDto
            {
                Name = $"{student.FirstName} {student.LastName}",
                DateOfBirth = student.DateOfBirth,
                Email = student.Email,
                Gender = student.Gender,
            };
            return new ApiResponse<ReadStudentDto>
            {
                IsSuccess = true,
                Data = studentDto,
                Message = "Student retrieved successfully",
            };
        }
        public async Task<ApiResponse<object>> UpdateStudentById(int id, CreateStudentDto studentDto, CancellationToken cancellationToken)
        {
            var student = await _studRepository.GetStudentByIdAsync(id,cancellationToken);
            if(student == null)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Student not found",
                };
            }
            student.FirstName = studentDto.FirstName;
            student.LastName = studentDto.LastName;
            student.Email = studentDto.Email;
            student.DateOfBirth = studentDto.DateOfBirth;
            student.Gender = studentDto.Gender;
            var result = await _studRepository.SaveChangesAsync();
            if (result.IsSuccess)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = true,
                    Message = "Student updated successfully",
                };
            }
            else
            {
                return new ApiResponse<object>
                {
                    IsSuccess = false,
                    Message = "Student update failed",
                };
            }
        }
        public async Task<ApiResponse<object>> DeleteStudentById(int id, CancellationToken cancellationToken)
        {
            var result = await _studRepository.DeleteStudentAsync(id, cancellationToken);
            if (result.IsSuccess)
            {
                return new ApiResponse<object>
                {
                    IsSuccess = true,
                    Message = "Student deleted successfully",
                };
            }
            return new ApiResponse<object>
            {
                IsSuccess = false,
                Message = "Student deletion failed",
            };
        }

    }
}
