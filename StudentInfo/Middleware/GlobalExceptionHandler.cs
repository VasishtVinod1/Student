using System.Net;

namespace Student_Management_System.Middleware
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                Console.WriteLine("Unhandled exception occurred while processing the request.");
                Console.WriteLine($"Message: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "text/plain";


                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");


            }
        }
    }
}
