using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class CourseService : NeptunService, ICourseService
{
    public CourseService(NeptunDbContext context) : base(context)
    {
    }

    public async Task<Course?> GetCourseById(Guid guid)
    {
        var course = await _context.Courses
            .FirstOrDefaultAsync(c => c.Id == guid);
        if (course == null)
        {
            throw new Exception($"Course with id {guid} not found");
        }
        return course;
    }

    public async Task<List<Course?>> GetAllCourses()
    {
        var courses = await _context.Courses.ToListAsync();
        return courses;
    }

    public async Task<Course> AddCourse(CreateCourseDTO course)
    {
        var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.NeptunCode == course.TeacherNeptunCode);
        if (teacher != null)
        {
            var convertedCourse = course.ToCourse(teacher);
            _context.Courses.Add(convertedCourse);
            await _context.SaveChangesAsync();
            return convertedCourse;
        }
       return null;
    }
}