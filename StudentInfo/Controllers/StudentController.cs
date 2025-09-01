//using Microsoft.AspNetCore.Mvc;
//using Student_Management_System.Data;
//using Student_Management_System.Domain;

//namespace Student_Management_System.Controllers
//{
//    [ApiController]
//    [Route("[controller]")]
//    public class StudentController : Controller
//    {
//        private readonly ILogger<StudentController> _logger;
//        private readonly IStudentRepository _repository;
//        public StudentController(ILogger<StudentController> logger, IStudentRepository repository)
//        {
//            _logger = logger;
//            _repository = repository;

//        }

//        [HttpGet]
//        public IActionResult GetAll()
//        {

//            return Ok(_repository.GetAll());
//        }

//        [HttpGet("{id}")]
//        public IActionResult GetById(int id)
//        {
//            var student = _repository.GetById(id);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            return Ok(student);
//        }

//        [HttpPut("{id}")]
//        public IActionResult Update(int id, Student updatedstudent)
//        {
//            var student = _repository.Update(id, updatedstudent);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            return Ok(student);


//        }

//        [HttpPost]
//        public IActionResult Add([FromBody] Student student)
//        {
//            _repository.Add(student);
//            return CreatedAtAction(nameof(GetById), new { id = student.Id }, student);

//        }

//        [HttpPatch("{id}")]
//        public IActionResult PartialUpdate(int id, Student updatedstudent)
//        {
//            var student = _repository.Update(id, updatedstudent);
//            if (student == null)
//            {
//                return NotFound();
//            }
//            return Ok(student);
//        }

//        [HttpDelete("{id}")]

//        public IActionResult Delete(int id)
//        {
//            var result = _repository.Delete(id);
//            if (result == false)
//            {
//                return NotFound();
//            }
//            return NoContent();
//        }


//    }
//}
