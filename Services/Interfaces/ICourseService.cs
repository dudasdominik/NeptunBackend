using NeptunBackend.Models;
using NeptunBackend.Models.DTO;

namespace NeptunBackend.Services.Interfaces;

public interface ICourseService
{
    Task<Course?> GetCourseById(Guid guid);
    Task<List<Course?>> GetAllCourses();
    Task<Course> AddCourse(CreateCourseDTO course);
}