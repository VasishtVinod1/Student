using Microsoft.AspNetCore.Mvc;

namespace Student_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet ("throw-exception")]
        public IActionResult ThrowException()
        {
            throw new Exception("This is a test exception to demonstrate the global exception handler .");
        }
    }
}
