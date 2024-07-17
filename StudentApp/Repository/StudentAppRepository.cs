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

        public async Task<ICollection<Student>> SearchStudents(StudentSearchModel searchModel)
        {
            var query = _context.Student.AsQueryable();

            if (!string.IsNullOrEmpty(searchModel.Name))
            {
                query = query.Where(s => s.Name.Contains(searchModel.Name));
            }
            if (!string.IsNullOrEmpty(searchModel.Email))
            {
                query = query.Where(s => s.Email.Contains(searchModel.Email));
            }
            if (!string.IsNullOrEmpty(searchModel.Course))
            {
                query = query.Where(s => s.Course.Contains(searchModel.Course));
            }
            if (!string.IsNullOrEmpty(searchModel.Address))
            {
                query = query.Where(s => s.Address.Contains(searchModel.Address));
            }
            if (!string.IsNullOrEmpty(searchModel.IdNumber))
            {
                query = query.Where(s => s.IdNumber == searchModel.IdNumber);
            }

            return await query.ToListAsync();
        }
    }

}