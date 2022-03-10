using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentApi.Models;
using StudentApi.Models.Request;
using StudentApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudents()
        {
            var students = await _studentService.GetStudents();

            return Ok(students);
        }

        [HttpGet("byQueryName")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IAsyncEnumerable<Student>>> GetStudentsByName([FromQuery] string name)
        {
            var students = await _studentService.GetStudentByName(name);

            if(students.Count() == 0)
                return NotFound();

            return Ok(students);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<Student>> GetStudent([FromRoute] Guid id)
        {
            var student = await _studentService.GetStudentById(id);

            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStudent([FromBody] StudentRequest request)
        {
            var response = await _studentService.CreateStudent(request); 

            return Created(nameof(CreateStudent), response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStudent([FromRoute] Guid id, [FromBody] StudentRequest request)
        {
            await _studentService.UpdateStudent(id, request);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent([FromRoute] Guid id)
        {
            var findStudent = _studentService.GetStudentById(id);
            if(findStudent == null)
                return NotFound();
                
            await _studentService.DeleteStudent(id);

            return NoContent();
        }
    }
}
