using Api.Data;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // api/students
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet ("GetAll")]
        public async Task<IEnumerable<Student>> GetStudents()
        {
            var students = await _context.Students.AsNoTracking().ToListAsync();

            return students;
        }

        [HttpPost ("Create")]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _context.AddAsync(student);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("Student Created Successfully");
            }
            return BadRequest();

        }

        [HttpGet("Fetch{id:int}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound("Student not found");
            }
            return Ok(student);
        }

        [HttpDelete("Delete{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _context.Remove(student);
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("Student Deleted Successfully");
            }
            return BadRequest();
        }

        [HttpPut("Update{id:int}")]
        public async Task<IActionResult> EditStudent(int id, Student student)
        {
            var studentFromDb = await _context.Students.FindAsync(id);
            if (studentFromDb is null)
            {
                return BadRequest("Student not found");
            }
            studentFromDb.Name = student.Name;
            studentFromDb.Address = student.Address;
            studentFromDb.PhoneNumber = student.PhoneNumber;
            studentFromDb.Email = student.Email;
            var result = await _context.SaveChangesAsync();
            if (result > 0)
            {
                return Ok("Student Updated Successfully");
            }
            return BadRequest("Student not updated");
        }


    }
}