using Microsoft.EntityFrameworkCore;
using StudentApi.Context;
using StudentApi.Models;
using StudentApi.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;

        public StudentService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStudent(int studentId)
        {
            Student student = await _context.Students.FindAsync(studentId);
            _context.Entry(student).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<Student> GetStudentById(int id)
        {
            var student = await _context.Students.FindAsync(id);
            return student;
        }

        public async Task<IEnumerable<Student>> GetStudentByName(string name)
        {
            try
            {
                IEnumerable<Student> students;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    students = await _context.Students.Where(x => x.Name.Contains(name)).ToListAsync();
                }
                else
                {
                    students = await GetStudents();
                }

                return students;
            }
            catch 
            {
                throw;
            }
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            try
            {
                return await _context.Students.ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateStudent(Student student)
        {
            _context.Entry(student).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
