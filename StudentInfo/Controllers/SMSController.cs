using Microsoft.AspNetCore.Mvc;
using SMS.Services.DTO;
using SMS.Services.Interfaces;

namespace SMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
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
            if (student == null)
                return NotFound();

            return Ok(student);
        }

      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] StudentCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = await _studentService.Add(dto);
            return Ok(created);
        }


     
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StudentUpdatedto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _studentService.Update(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

       
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] StudentUpdatedto dto)
        {
            var updated = await _studentService.Patch(id, dto);
            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _studentService.DeleteById(id);
            if (deleted == null)
                return NotFound();

            return NoContent();
        }
    }
}
