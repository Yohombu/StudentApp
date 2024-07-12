using StudentApp.Models;
using StudentApp.Models.Entities;

namespace StudentApp.Interfaces
{
    public interface IStudentAppRepository
    {
        Task AddStudent(Student student);
        Task<ICollection<Student>> GetStudent();
        Task DeleteStudent(Student student);
        Task<Student> GetStudentById(int id);
    }
}