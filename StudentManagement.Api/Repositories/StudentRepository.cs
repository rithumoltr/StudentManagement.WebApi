using Microsoft.EntityFrameworkCore;
using StudentManagement.Api.Common;
using StudentManagement.Api.Data;

namespace StudentManagement.Api.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStudentAsync(Student student)
        {
           await _context.Students.AddAsync(student);
        }
        public async Task<OperationResult> SaveChangesAsync()
        {
            int rowsAffected = await _context.SaveChangesAsync();
                return new OperationResult
                {
                    IsSuccess = rowsAffected>0?true:false,
                };
        }
        public async Task<bool> EmailExist(string email)
        {
            return await _context.Students.AnyAsync(s => s.Email == email);
        }
        public async Task<List<Student>> GetAllStudents(CancellationToken token)
        {
            var students = await _context.Students.AsNoTracking().ToListAsync(token);
            return students;
          
        }
        public async Task<Student> GetStudentByIdAsync(int id, CancellationToken token)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id,token);
            return student;
        }
        public async Task<OperationResult> DeleteStudentAsync(int id, CancellationToken token)
        {
            var student = await _context.Students.FirstOrDefaultAsync(s => s.StudentId == id);
            if(student != null)
            {
                _context.Students.Remove(student);
                int rowsAffected = await _context.SaveChangesAsync();
                return new OperationResult
                {
                    IsSuccess = rowsAffected > 0 ?true : false,

                };
            }
            throw new NotFoundException($"Student with id {id} not found");
        }
    }
}
