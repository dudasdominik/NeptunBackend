using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class ExamController : ControllerBase
{
    private readonly IExamService _examService;
    
    public ExamController(IExamService examService)
    {
        _examService = examService;
    }
    [HttpGet("{guid}")]
    public async Task<IActionResult> GetExamById(Guid guid)
    {
        var exam = await _examService.GetExamById(guid);
        if (exam == null)
        {
            return NotFound();
        }
        return Ok(exam);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllExams()
    {
        var exams = await _examService.GetAllExams();
        if (exams == null || exams.Count == 0)
        {
            return NotFound();
        }
        return Ok(exams);
    }
    
    [HttpPost("add")]
    public async Task<IActionResult> AddExam([FromBody] CreateExamDTO exam)
    {
        if (exam == null)
        {
            return BadRequest("Exam cannot be null");
        }
        
        var addedExam = await _examService.AddExam(exam);
        if (addedExam == null)
        {
            return BadRequest("Failed to add exam");
        }

        return Ok(addedExam);
    }
    [HttpDelete("delete/{guid}")]
    public async Task<IActionResult> DeleteExam(Guid guid)
    {
        var result = await _examService.DeleteExam(guid);
        if (!result)
        {
            return NotFound();
        }
        return Ok(true);
    }
    [HttpPut("update/{guid}")]
    public async Task<IActionResult> UpdateExam(Guid guid, [FromBody] Exam exam)
    {
        if (exam == null)
        {
            return BadRequest("Exam cannot be null");
        }
        
        var result = await _examService.UpdateExam(guid, exam);
        if (!result)
        {
            return NotFound();
        }
        return Ok(true);
    }
    [HttpGet("course/{courseId}")]
    public async Task<IActionResult> GetExamsByCourseId(Guid courseId)
    {
        var exams = await _examService.GetExamsByCourseId(courseId);
        if (exams == null || exams.Count == 0)
        {
            return NotFound();
        }
        return Ok(exams);
    }
    [HttpGet("teacher/{teacherId}")]
    public async Task<IActionResult> GetExamsByTeacherId(string teacherId)
    {
        var exams = await _examService.GetExamsByTeacherId(teacherId);
        if (exams == null || exams.Count == 0)
        {
            return NotFound();
        }
        return Ok(exams);
    }
}