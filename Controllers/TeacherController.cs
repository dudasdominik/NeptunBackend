using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;
        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }
        
        [HttpGet("{neptunCode}")]
        public async Task<IActionResult> GetTeacherByNeptunCode(string neptunCode)
        {
            var teacher = await _teacherService.GetTeacherByNeptunCode(neptunCode);
            if (teacher == null)
            {
                Console.Write("szar");
                return NotFound();
            }
            return Ok(teacher);
        }
        
        [HttpGet("all")]
        public async Task<IActionResult> GetAllTeachers()
        {
            var teachers = await _teacherService.GetAllTeachers();

            _logger.LogInformation("Fetched {Count} teacher(s) from database.", teachers?.Count ?? 0);

            if (teachers == null || teachers.Count == 0)
            {
                _logger.LogWarning("No teachers found in database.");
                return NotFound();
            }

            return Ok(teachers);
        }
        
        [HttpPost("add")]
        public async Task<IActionResult> AddTeacher([FromBody] CreateTeacherDTO teacher)
        {
            if (teacher == null)
            {
                return BadRequest("Teacher cannot be null");
            }
            
            var addedTeacher = await _teacherService.AddTeacher(teacher);
            if (addedTeacher == null)
            {
                return BadRequest("Failed to add teacher");
            }

            return Ok(addedTeacher);
        }
}