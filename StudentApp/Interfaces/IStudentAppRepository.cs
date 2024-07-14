using StudentApp.Models;
using StudentApp.Models.Entities;

namespace StudentApp.Interfaces
{
    public interface IStudentAppRepository
    {
        Task AddStudent(Student student);
        Task<ICollection<Student>> GetStudent();
        Task<Student> GetStudentById(int id);
        Task UpdateStudent(Student student);
    }
}