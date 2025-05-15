using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Controllers;
[ApiController]
[Route("api/[controller]/")]
public class ExamRegistrationController : ControllerBase
{
    private readonly IExamRegistrationService _examRegistrationService;
    
    public ExamRegistrationController(IExamRegistrationService examRegistrationService)
    {
        _examRegistrationService = examRegistrationService;
    }
    [HttpGet("{guid}")]
    public async Task<IActionResult> GetExamRegistrationById(Guid guid)
    {
        var examRegistration = await _examRegistrationService.GetExamRegistrationById(guid);
        if (examRegistration == null)
        {
            return NotFound();
        }
        return Ok(examRegistration);
    }
    [HttpGet("all")]
    public async Task<IActionResult> GetAllExamRegistrations()
    {
        var examRegistrations = await _examRegistrationService.GetAllExamRegistrations();
        if (examRegistrations == null || examRegistrations.Count == 0)
        {
            return NotFound();
        }
        return Ok(examRegistrations);
    }
    [HttpPost("add")]
    public async Task<IActionResult> AddExamRegistration([FromBody] ExamRegistration examRegistration)
    {
        if (examRegistration == null)
        {
            return BadRequest("Exam registration cannot be null");
        }
        
        var addedExamRegistration = await _examRegistrationService.AddExamRegistration(examRegistration);
        if (addedExamRegistration == null)
        {
            return BadRequest("Failed to add exam registration");
        }

        return Ok(addedExamRegistration);
    }
    [HttpDelete("delete/{guid}")]
    public async Task<IActionResult> DeleteExamRegistration(Guid guid)
    {
        var result = await _examRegistrationService.DeleteExamRegistration(guid);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpPut("update/{guid}")]
    public async Task<IActionResult> UpdateExamRegistration(Guid guid, [FromBody] ExamRegistration examRegistration)
    {
        if (examRegistration == null)
        {
            return BadRequest("Exam registration cannot be null");
        }
        
        var result = await _examRegistrationService.UpdateExamRegistration(guid, examRegistration);
        if (!result)
        {
            return NotFound();
        }
        return Ok(result);
    }
    [HttpGet("student/{neptunCode}")]
    public async Task<IActionResult> GetExamRegistrationsByStudentId(string neptunCode)
    {
        var examRegistrations = await _examRegistrationService.GetExamRegistrationsByStudentId(neptunCode);
        if (examRegistrations == null || examRegistrations.Count == 0)
        {
            return NotFound();
        }
        return Ok(examRegistrations);
    }
    [HttpGet("exam/{guid}")]
    public async Task<IActionResult> GetExamRegistrationsByExamId(Guid guid)
    {
        var examRegistrations = await _examRegistrationService.GetExamRegistrationsByExamId(guid);
        if (examRegistrations == null || examRegistrations.Count == 0)
        {
            return NotFound();
        }
        return Ok(examRegistrations);
    }
   
    

    
}