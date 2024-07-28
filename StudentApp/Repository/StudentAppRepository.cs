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

        public async Task<Student> GetStudentById(int id)
        {
            return await _context.Student.FindAsync(id);
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
        public async Task DeleteStudent(Student student)
        {
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Student.Update(student);
            await _context.SaveChangesAsync();
        }
    }

}