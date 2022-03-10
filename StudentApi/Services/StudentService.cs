using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentApi.Context;
using StudentApi.Models;
using StudentApi.Models.Request;
using StudentApi.Models.Response;
using StudentApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public StudentService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentResponse> CreateStudent(StudentRequest student)
        {
            var studentMap = _mapper.Map<Student>(student);

            var result = _context.Students.Add(studentMap).Entity;
            await _context.SaveChangesAsync();

            return _mapper.Map<StudentResponse>(studentMap);
        }

        public async Task DeleteStudent(Guid studentId)
        {
            Student student = await _context.Students.FindAsync(studentId);
            _context.Entry(student).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<StudentResponse> GetStudentById(Guid id)
        {
            var student = await _context.Students.FindAsync(id);
            return _mapper.Map<StudentResponse>(student);
        }

        public async Task<IEnumerable<StudentResponse>> GetStudentByName(string name)
        {
            try
            {
                IEnumerable<StudentResponse> students;
                if (!string.IsNullOrWhiteSpace(name))
                {
                    var result = await _context.Students.Where(x => x.Name.Contains(name)).ToListAsync();
                    students = _mapper.Map<IEnumerable<StudentResponse>>(result);
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

        public async Task<IEnumerable<StudentResponse>> GetStudents()
        {
            try
            {
                var response = await _context.Students.ToListAsync();
                return _mapper.Map<IEnumerable<StudentResponse>>(response);
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdateStudent(Guid studentId, StudentRequest student)
        {
            var studentObj = new Student()
            {
                Id = studentId,
                Email = student.Email,
                Name = student.Name,
                Age = student.Age
            };

            _context.Entry(studentObj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
