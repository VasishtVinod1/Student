using Microsoft.AspNetCore.Mvc;

namespace Student_Management_System.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
        [HttpGet ("throw-exception")]
        public IActionResult ThrowException()
        {
            _logger.LogInformation("ThrowException endpoint was called.");

            throw new Exception("This is a test exception to demonstrate the global exception handler .");  
        }
    }
}
