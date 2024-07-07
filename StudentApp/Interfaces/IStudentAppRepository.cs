using StudentApp.Models;
using StudentApp.Models.Entities;

namespace StudentApp.Interfaces
{
    public interface IStudentAppRepository
    {
        void AddStudent(Student newStudent);
        ICollection<Student> GetStudent();
    }
}