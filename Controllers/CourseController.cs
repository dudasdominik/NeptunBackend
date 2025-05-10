using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class CourseController : ControllerBase
{
    private readonly ICourseService _courseService;
    
    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }
    
    [HttpGet("{guid}")]
    public async Task<IActionResult> GetCourseById(Guid guid)
    {
        var course = await _courseService.GetCourseById(guid);
        if (course == null)
        {
            Console.Write("szar");
            return NotFound();
        }
        return Ok(course);
    }
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllCourses()
    {
        var courses = await _courseService.GetAllCourses();
        if (courses == null || courses.Count == 0)
        {
            Console.Write("szar");
            return NotFound();
        }
        return Ok(courses);
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddCourse([FromBody] CreateCourseDTO course)
    {
        if (course == null)
        {
            return BadRequest("Course cannot be null");
        }
        
        var addedCourse = await _courseService.AddCourse(course);
        if (addedCourse == null)
        {
            return BadRequest("Failed to add course");
        }

        return Ok(addedCourse);
    }
}