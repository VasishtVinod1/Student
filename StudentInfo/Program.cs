using Serilog;
using SMS.Services.Implementation;
using SMS.Services.Interfaces;
using Student_Management_System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using SMS.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    //builder.Services.AddAutoMapper(typeof(StudentProfile));


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddScoped<IStudentRepository, StudentRepository>();
    builder.Services.AddScoped<IStudentService, StudentServices>();

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
