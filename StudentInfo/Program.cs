using Serilog;
using Student_Management_System.Data;

try
{
    // 1. Configure Serilog before builder
    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
        .Enrich.FromLogContext()
        .CreateLogger();

    // 2. Create builder
    var builder = WebApplication.CreateBuilder(args);

    // Hook Serilog into logging
    builder.Host.UseSerilog();

    // Add services
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<IStudentRepository , StudentRepository>();

    var app = builder.Build();

    // 3. Register Global Exception Middleware
    app.UseMiddleware<Student_Management_System.Middleware.GlobalExceptionHandler>();
    

    // Pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
