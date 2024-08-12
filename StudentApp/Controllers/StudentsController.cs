using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;
using StudentApp.Models.Dtos;
using StudentApp.Repository;
using Microsoft.EntityFrameworkCore;


namespace StudentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentAppRepository _studentAppRepository;
        private readonly ApplicationDbContext context;


        public StudentsController(IStudentAppRepository studentAppRepository, ApplicationDbContext context)
        {
            _studentAppRepository = studentAppRepository;
            this.context = context;
        }
        [HttpPost("add")] // Example route to handle POST request to add a student
        public async Task<ActionResult<ICollection<Student>>> AddStudent([FromBody] CreateStudentDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Need to fill required fields");
            }

            // Check if a student with the same ID number, email, name, and course already exists
            var existingStudentBySameDetails = await context.Student
                .FirstOrDefaultAsync(s => s.IdNumber == model.IdNumber && s.Email == model.Email && s.Name == model.Name && s.Course == model.Course);

            if (existingStudentBySameDetails != null)
            {
                var errorResponse = new
                {
                    StatusCode = 403,
                    Message = "This student has already registered"
                };
                return new ObjectResult(errorResponse) { StatusCode = 403 };
            }

            // Check if a student with the same ID number, email, and name exists
            var existingStudentByIdEmailName = await context.Student
                .FirstOrDefaultAsync(s => s.IdNumber == model.IdNumber && s.Email == model.Email && s.Name == model.Name);

            if (existingStudentByIdEmailName == null)
            {
                // Proceed to further checks only if existingStudentByIdEmailName is null
                var existingStudentByIdEmail = await context.Student
                    .FirstOrDefaultAsync(s => s.IdNumber == model.IdNumber && s.Email == model.Email);

                if (existingStudentByIdEmail != null)
                {
                    var errorResponse = new
                    {
                        StatusCode = 403,
                        Message = "A student with this ID Number and Email already exists."
                    };
                    return new ObjectResult(errorResponse) { StatusCode = 403 };
                }

                var existingStudentById = await context.Student
                    .FirstOrDefaultAsync(s => s.IdNumber == model.IdNumber);

                if (existingStudentById != null)
                {
                    var errorResponse = new
                    {
                        StatusCode = 403,
                        Message = "This ID has already registered"
                    };
                    return new ObjectResult(errorResponse) { StatusCode = 403 };
                }

                var existingStudentByEmail = await context.Student
                    .FirstOrDefaultAsync(s => s.Email == model.Email);

                if (existingStudentByEmail != null)
                {
                    var errorResponse = new
                    {
                        StatusCode = 403,
                        Message = "This Email has already registered"
                    };
                    return new ObjectResult(errorResponse) { StatusCode = 403 };
                }
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

            await _studentAppRepository.AddStudent(newStudent);

            return Ok("Student added successfully");
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public async Task<ActionResult<ICollection<Student>>> GetStudent()
        {
            var students = await _studentAppRepository.GetStudent();

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
            var student = await _studentAppRepository.GetStudentById(id);

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
            var studentToDelete = await _studentAppRepository.GetStudentById(Id);
            if (studentToDelete == null)
            {
                return NotFound("Student not found");
            }

            await _studentAppRepository.DeleteStudent(studentToDelete);

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

            var studentToUpdate = await _studentAppRepository.GetStudentById(Id);

            if (studentToUpdate == null)
            {
                return NotFound("Student not found");
            }

            UpdateStudentProperties(studentToUpdate, updatedStudent);

            await _studentAppRepository.UpdateStudent(studentToUpdate);

            return Ok("Student updated successfully");
        }

        private void UpdateStudentProperties(Student studentToUpdate, UpdateStudentDto updatedStudent)
        {
            studentToUpdate.Name = updatedStudent.Name ?? studentToUpdate.Name;
            studentToUpdate.Email = updatedStudent.Email ?? studentToUpdate.Email;
            studentToUpdate.Course = !string.IsNullOrEmpty(updatedStudent.Course) ? updatedStudent.Course : studentToUpdate.Course;
            studentToUpdate.Address = !string.IsNullOrEmpty(updatedStudent.Address) ? updatedStudent.Address : studentToUpdate.Address;
            studentToUpdate.IdNumber = updatedStudent.IdNumber ?? studentToUpdate.IdNumber;
        }

    }
}