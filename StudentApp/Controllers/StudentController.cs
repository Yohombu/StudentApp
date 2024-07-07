using Microsoft.AspNetCore.Mvc;
using StudentApp.Data;
using StudentApp.Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentApp.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public StudentController(ApplicationDbContext db)
        {
            _db = db;   
        }
        //public IActionResult Index()
        //{
        //    IEnumerable<Student> StudentList = _db.Student;
        //    return View(StudentList);
        //}

        
    }
}
