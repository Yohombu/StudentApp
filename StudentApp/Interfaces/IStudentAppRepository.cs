using StudentApp.Models;
using StudentApp.Models.Entities;
using StudentApp.Repository;

namespace StudentApp.Interfaces
{
    public interface IStudentAppRepository
    {
        Task AddStudent(Student student);
        Task<ICollection<Student>> GetStudent();
        Task<Student> GetStudentById(int id);
        Task DeleteStudent(Student student);
        Task UpdateStudent(Student student);
    }
}