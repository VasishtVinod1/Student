using Microsoft.AspNetCore.Mvc;
using SMS.Domain.Repositories;   
using SMS.Services.Interfaces;   
using SMS.Services.DTO;        

namespace Student_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SMSController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public SMSController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var students = await _studentService.GetAll();
            return Ok(students); 
        }

       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var student = await _studentService.GetById(id);
            if (student == null) return NotFound();

            return Ok(student); 
        }

        
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentDTO_POST studentdtopost)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var created = await _studentService.Add(studentdtopost);

            return Ok(studentdtopost);
        }

       
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentDTO_POST studentDto)
        {
            if (id != studentDto.StudentId) return BadRequest("ID mismatch");

            var updated = await _studentService.Update(studentDto);
            if (updated == null) return NotFound();

            return Ok(updated);
        }

       
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] StudentDTO_POST studentDto)
        {
            if (studentDto == null)
                return BadRequest("No updates provided");

            var patched = await _studentService.Patch(id, studentDto);
            if (patched == null) return NotFound();

            return Ok(patched);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _studentService.DeleteById(id);
            if (deleted == null) return NotFound();

            return NoContent();
        }
    }
}
