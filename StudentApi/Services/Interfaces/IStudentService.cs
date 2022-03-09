using StudentApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetStudents();
        Task<Student> GetStudentById(int id);
        Task<IEnumerable<Student>> GetStudentByName(string name);
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student);
        Task DeleteStudent(int studentId);
    }
}
