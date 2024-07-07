using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;

namespace StudentApp.Repository
{
    public class StudentAppRepository:IStudentAppRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentAppRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void AddStudent(Student student)
        {
            _context.Student.Add(student);
            _context.SaveChanges();
        }
        public ICollection<Student> GetStudent()
        {
            return _context.Student.OrderBy(student=>student.Id).ToList();
        }
    }

   
}