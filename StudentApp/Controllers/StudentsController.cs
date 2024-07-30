using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;
using StudentApp.Repository;


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
        [HttpPost("add")] 
        public async Task<ActionResult<ICollection<Student>>> AddStudent([FromBody] Student model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        public async Task<ActionResult<ICollection<Student>>> UpdateStudent(int Id, [FromBody] Student updatedStudent)
        {
            if (updatedStudent == null)
            {
                return BadRequest("Updated student information cannot be null.");
            }

            var studentToUpdate = await _studentAppRepository.GetStudentById(Id);

            if (studentToUpdate == null)
            {
                return BadRequest("Student not found");
            }

            updatedStudent.Id = Id;

            UpdateStudentProperties(studentToUpdate, updatedStudent);

            await _studentAppRepository.UpdateStudent(studentToUpdate);

            return Ok("Student Updated successfully");
        }
        private void UpdateStudentProperties(Student studentToUpdate, Student updatedStudent)
        {
            studentToUpdate.Name = updatedStudent.Name;
            studentToUpdate.Email = updatedStudent.Email;
            studentToUpdate.Course = updatedStudent.Course;
            studentToUpdate.Address = updatedStudent.Address;
            studentToUpdate.IdNumber = updatedStudent.IdNumber;
        }

    }
}