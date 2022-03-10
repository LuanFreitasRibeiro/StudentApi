using StudentApi.Models;
using StudentApi.Models.Request;
using StudentApi.Models.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudentApi.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<StudentResponse>> GetStudents();
        Task<StudentResponse> GetStudentById(Guid id);
        Task<IEnumerable<StudentResponse>> GetStudentByName(string name);
        Task<StudentResponse> CreateStudent(StudentRequest student);
        Task UpdateStudent(Guid studentId, StudentRequest student);
        Task DeleteStudent(Guid studentId);
    }
}
