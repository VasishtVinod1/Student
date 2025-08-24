using System.Net;

namespace Student_Management_System.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Unhandled exception occurred while processing the request.");
                //Console.WriteLine($"Message: {ex.Message}");
                //Console.WriteLine($"Stack trace: {ex.StackTrace}");
                _logger.LogError(ex, "Unhandled exception occurred while processing the request.");

                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.Response.ContentType = "application/json";


                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");


            }
        }
    }
}
