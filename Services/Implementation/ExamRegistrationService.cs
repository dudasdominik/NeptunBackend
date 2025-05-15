using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class ExamRegistrationService : NeptunService, IExamRegistrationService
{
    public ExamRegistrationService(NeptunDbContext context) : base(context)
    {
    }

    public async Task<ExamRegistration?> GetExamRegistrationById(Guid guid)
    {
        var examRegistration = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .FirstOrDefaultAsync(er => er.Id == guid);
        return examRegistration;
    }

    public async Task<List<ExamRegistration?>> GetAllExamRegistrations()
    {
        return await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .ToListAsync();
    }

    public async Task<ExamRegistration> AddExamRegistration(ExamRegistration examRegistration)
    {
         _context.ExamRegistrations.Add(examRegistration);
        await _context.SaveChangesAsync();
        return examRegistration;
    }

    public async Task<bool> DeleteExamRegistration(Guid guid)
    {
        var examRegistration = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .FirstOrDefaultAsync(er => er.Id == guid);
        if (examRegistration == null)
        {
            throw new Exception($"Exam registration with id {guid} not found");
        }
        _context.ExamRegistrations.Remove(examRegistration);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateExamRegistration(Guid guid, ExamRegistration examRegistration)
    {
        var existingExamRegistration = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .FirstOrDefaultAsync(er => er.Id == guid);
        existingExamRegistration.Grade = examRegistration.Grade;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<ExamRegistration>> GetExamRegistrationsByStudentId(string studentId)
    {
        var examRegistrations =await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .Where(er => er.Student.NeptunCode == studentId)
            .ToListAsync();
        return examRegistrations;
    }

    public async Task<List<ExamRegistration>> GetExamRegistrationsByExamId(Guid examId)
    {
        var examRegistrations = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .Where(er => er.ExamId == examId)
            .ToListAsync();
        return examRegistrations;
    }

    public async Task<List<ExamRegistration>> GetExamRegistrationsByCourseId(Guid courseId)
    {
        var examRegistrations = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .Where(er => er.Exam.CourseId == courseId)
            .ToListAsync();
        return examRegistrations;
    }

    public async Task<List<ExamRegistration>> GetExamRegistrationsByTeacherId(string teacherId)
    {
        var examRegistrations = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .Where(er => er.Exam.Course.Teacher.NeptunCode == teacherId)
            .ToListAsync();
        return examRegistrations;
    }

    public async Task<ExamRegistration> GradeExam(int grade, Guid id)
    {
        var examRegistration = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .Include(er => er.Student)
            .FirstOrDefaultAsync(er => er.Id == id);
        if (examRegistration == null)
        {
            throw new Exception($"Exam registration with id {id} not found");
        }
        examRegistration.Grade = grade;
        await _context.SaveChangesAsync();
        return examRegistration;
    }

    public async Task<bool> TeacherOwnsExamReg(string teacherCode, Guid examId)
    {
        var registration = await _context.ExamRegistrations
            .Include(er => er.Exam)
            .ThenInclude(e => e.Course)
            .FirstOrDefaultAsync(er => er.Id == examId);
        if (registration == null)
        {
            throw new Exception($"Exam registration with id {examId} not found");
        }
        return true;
    }
}