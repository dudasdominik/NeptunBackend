using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class StudentController : ControllerBase
{
  private readonly IStudentService _studentService;
  
  
  public StudentController(IStudentService studentService)
  {
    _studentService = studentService;
  }

  [HttpGet("{neptunCode}")]
  public async Task<IActionResult> GetStudentByNeptunCode(string neptunCode)
  {
    var student = await _studentService.GetStudentByNeptunCode(neptunCode);
    if (student == null || student.NeptunCode != neptunCode)
    {
      Console.Write("szar");
      return NotFound();
    }
    return Ok(student);
  }
  
  
  [HttpGet("all")]
  public async Task<IActionResult> GetAllStudents()
  {
    var students = await _studentService.GetAllStudents();
    if (students == null || students.Count == 0)
    {
      return NotFound();
    }
    return Ok(students);
  }
  
  [HttpPost("add")]
  public async Task<IActionResult> AddStudent([FromBody] CreateStudentDTO student)
  {
    if (student == null)
    {
      return BadRequest("Student cannot be null");
    }
    
    var addedStudent = await _studentService.AddStudent(student);
    if (addedStudent == null)
    {
      return BadRequest("Failed to add student");
    }

    return Ok(addedStudent);
  }
  [HttpPut("update/{neptunCode}")]
  public async Task<IActionResult> UpdateStudent(string neptunCode, [FromBody] UpdateStudentDTO student)
  {
    if (student == null)
    {
      return BadRequest("Student cannot be null");
    }
    
    var updatedStudent = await _studentService.UpdateStudent(neptunCode, student);
    if (updatedStudent == null)
    {
      return BadRequest("Failed to update student");
    }

    return Ok(updatedStudent);
  }
  
  [HttpDelete("delete/{neptunCode}")]
  public async Task<IActionResult> DeleteStudent(string neptunCode)
  {
    var deleted = await _studentService.DeleteStudent(neptunCode);
    if (!deleted)
    {
      return NotFound();
    }
    return Ok();
  }
  
  [HttpPost("login")]
  public async Task<IActionResult> LogInStudent(string neptunCode, string password)
  {
    
    var loggedInStudent = await _studentService.LogInStudent(neptunCode, password);
    if (loggedInStudent == null)
    {
      return NotFound();
    }

    return Ok(loggedInStudent);
  }
  
  [HttpPut("updatePassword/{neptunCode}")]
  public async Task<IActionResult> UpdateStudentPassword(string neptunCode, string currentPassword, string newPassword)
  {
    var updatedStudent = await _studentService.UpdateStudentPassword(neptunCode, currentPassword, newPassword);
    if (updatedStudent == null)
    {
      return BadRequest("Failed to update student password");
    }

    return Ok(updatedStudent);
  }
  
  [HttpPost("enrollCourse/{neptunCode}")]
  public async Task<IActionResult> EnrollCourse(string neptunCode, Guid courseId)
  {
    var enrolledStudent = await _studentService.EnrollCourse(neptunCode, courseId);
    if (enrolledStudent == null)
    {
      return BadRequest("Failed to enroll student in course");
    }

    return Ok(enrolledStudent);
  }
    
}