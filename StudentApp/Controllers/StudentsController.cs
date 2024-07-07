using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;
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
        public StudentsController(IStudentAppRepository studentAppRepository, ApplicationDbContext context){
            _istudentAppRepository = studentAppRepository;
            this.context  =  context;
        }
        [HttpPost("add")] // Example route to handle POST request to add a student
        public IActionResult AddStudent([FromBody] Student model)
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
                Address = model.Address
            };

            // Add the student to the repository
            _istudentAppRepository.AddStudent(newStudent);

            // Return a success response or appropriate status code
            return Ok("Student added successfully");
        }
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>))]
        public IActionResult GetStudent()
        {
            var students = _istudentAppRepository.GetStudent();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }
    }


   



    
}
