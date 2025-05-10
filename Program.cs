using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Services;
using NeptunBackend.Services.Implementation;
using NeptunBackend.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();

var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    Console.WriteLine("❌ Connection string is NULL or empty.");
    throw new Exception("Missing DB connection string!");
}
else
{
    Console.WriteLine($"✅ Connection string: {connectionString}");
}


builder.Services.AddDbContext<NeptunDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<ITeacherService, TeacherService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<NeptunService>();


var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();