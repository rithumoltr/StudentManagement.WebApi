using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Api.DTO;
using StudentManagement.Api.Services;

namespace StudentManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _student;

        public StudentController(IStudentService student)
        {
            _student = student;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] CreateStudentDto student,CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _student.AddStudent(student,cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetAllStudents")]
        public async Task<IActionResult> GetAllStudentsAsync(CancellationToken cancellationToken)
        {
            var result = await _student.GetAllStudentsAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudentByIdAsync([FromQuery] int id,CancellationToken cancellationToken)
        {
            var result = await _student.GetStudentByIdAsync(id,cancellationToken);
            return Ok(result);
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudentById([FromQuery] int id, [FromBody] CreateStudentDto studentDto, CancellationToken cancellationToken)
        {
            var result = await _student.UpdateStudentById(id, studentDto, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("DeleteStudent")]
        public async Task<IActionResult> DeleteStudentById(int id, CancellationToken cancellationToken)
        {
            var result = await _student.DeleteStudentById(id, cancellationToken);
            return Ok(result);
        }
    }
}
