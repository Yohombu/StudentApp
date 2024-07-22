using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;
using StudentApp.Models.Dtos;
using StudentApp.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentAppRepository _istudentAppRepository;
        private readonly ApplicationDbContext context;


        // GET: api/<StudentsController>
        public StudentsController(IStudentAppRepository studentAppRepository, ApplicationDbContext context)
        {
            _istudentAppRepository = studentAppRepository;
            this.context = context;
        }
        [HttpPost("add")] // Example route to handle POST request to add a student
        public async Task<ActionResult<ICollection<Student>>> AddStudent([FromBody] CreateStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Assuming StudentInputModel is a class that represents incoming data
            // Here you create a Student object based on incoming data
            Student newStudent = new Student
            {
                Name = model.Name,
                Email = model.Email,
                Course = model.Course,
                Address = model.Address,
                IdNumber = model.IdNumber,
            };

            // Add the student to the repository
            await _istudentAppRepository.AddStudent(newStudent);

            // Return a success response or appropriate status code
            return Ok("Student added successfully");
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public async Task<ActionResult<ICollection<Student>>> GetStudent()
        {
            var students = await _istudentAppRepository.GetStudent();

            if (students == null || students.Count == 0)
            {
                return NotFound();
            }

            return Ok(students);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _istudentAppRepository.GetStudentById(id);

            if (student == null)
            {
                return NotFound("Student not found");
            }

            return Ok(student);
        }

        [HttpDelete("{Id}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<ICollection<Student>>> DeleteStudent(int Id)
        {
            var studentToDelete = await _istudentAppRepository.GetStudentById(Id);
            if (studentToDelete == null)
            {
                return NotFound("Student not found");
            }

            await _istudentAppRepository.DeleteStudent(studentToDelete);

            return Ok("Student Deleted successfully");
        }

        [HttpPut("{Id}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public async Task<ActionResult> UpdateStudent(int Id, [FromBody] UpdateStudentDto updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest("Empty request");
            }

            var studentToUpdate = await _istudentAppRepository.GetStudentById(Id);

            if (studentToUpdate == null)
            {
                return NotFound("Student not found");
            }

            UpdateStudentProperties(studentToUpdate, updatedStudent);

            await _istudentAppRepository.UpdateStudent(studentToUpdate);

            return Ok("Student updated successfully");
        }

        private void UpdateStudentProperties(Student studentToUpdate, UpdateStudentDto updatedStudent)
        {
            studentToUpdate.Name = updatedStudent.Name ?? studentToUpdate.Name;
            studentToUpdate.Email = updatedStudent.Email ?? studentToUpdate.Email;
            studentToUpdate.Course = updatedStudent.Course ?? studentToUpdate.Course;
            studentToUpdate.Address = updatedStudent.Address ?? studentToUpdate.Address;
            studentToUpdate.IdNumber = updatedStudent.IdNumber ?? studentToUpdate.IdNumber;
        }

    }
}