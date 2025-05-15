using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;

[ApiController]
[Route("api/[controller]/")]
[Authorize(Roles = "Teacher")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;
    private readonly IExamRegistrationService _examRegistrationService;
        public TeacherController(ITeacherService teacherService, IExamRegistrationService examRegistrationService)
        {
            _teacherService = teacherService;
            _examRegistrationService = examRegistrationService;
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

            if (teachers == null || teachers.Count == 0)
            {
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
        [HttpPost("{neptunCode}/{courseId}/createexam")]
        public async Task<IActionResult> CreateExam([FromBody] CreateExamDTO exam, Guid courseId, string neptunCode)
        {
            if (exam == null)
            {
                return BadRequest("Exam cannot be null");
            }
            
            var createdExam = await _teacherService.CreateExamForCourse(exam, courseId, neptunCode);
            if (createdExam == null)
            {
                return BadRequest("Failed to create exam");
            }

            return Ok(createdExam);
        }
        [HttpPut("{id}/grade")]
        public async Task<IActionResult> GradeExam(Guid id, [FromBody] GradeExamDTO dto)
        {
            var teacherNeptunCode = User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType);
            if (string.IsNullOrEmpty(teacherNeptunCode))
            {
                return Unauthorized(User.FindFirstValue(ClaimsIdentity.DefaultNameClaimType));
            }

            var isAuthorized = await _examRegistrationService.TeacherOwnsExamReg(teacherNeptunCode, id);
            if (!isAuthorized)
            {
                return Forbid("You are not authorized to grade this exam.");
            }
            

            var result = await _examRegistrationService.GradeExam(dto.Grade, id);
            if (result == null)
            {
                return BadRequest("Grading failed.");
            }

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LogIn([FromBody] LoginDetailsDTO login)
        {
            if (login == null)
            {
                return BadRequest("Login cannot be null");
            }
            
            var token = await _teacherService.LogIn(login.NeptunCode, login.Password);
            if (token == null)
            {
                return BadRequest("Failed to log in");
            }

            return Ok(token);
        }
}