using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Interfaces;
using StudentApp.Models.Entities;

namespace StudentApp.Repository
{
    public class StudentAppRepository : IStudentAppRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentAppRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddStudent(Student student)
        {
            _context.Student.Add(student);
            await _context.SaveChangesAsync();
        }
        public async Task<ICollection<Student>> GetStudent()
        {
            return await _context.Student.ToListAsync();
        }
    }


}