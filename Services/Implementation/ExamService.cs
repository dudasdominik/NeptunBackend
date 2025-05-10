using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class ExamService : NeptunService, IExamService
{
    public ExamService(NeptunDbContext context) : base(context)
    {
    }

    public async Task<Exam?> GetExamById(Guid guid)
    {
        var exam = await _context.Exams
            .FirstOrDefaultAsync(e => e.Id == guid);
        if (exam == null)
        {
            throw new Exception($"Exam with id {guid} not found");
        }
        return exam;
    }

    public async Task<List<Exam?>> GetAllExams()
    {
        var exams = await _context.Exams
            .Include(e => e.Course)
            .Include(e => e.ExamRegistrations)
            .ThenInclude(er => er.Student)
            .ToListAsync();
        return exams;
    }

    public async Task<Exam> AddExam(CreateExamDTO exam)
    {
        var course = await _context.Courses
            .Include(c => c.Teacher)
            .FirstOrDefaultAsync(c => c.Id == exam.CourseId);
        if (course == null)
        {
            throw new Exception($"Course with id {exam.CourseId} not found");
        }
        var convertedExam = exam.ToExam(course);
        _context.Exams.Add(convertedExam);
        _context.SaveChanges();
        return convertedExam;
    }

    public async Task<bool> DeleteExam(Guid guid)
    {
        var exam = await _context.Exams
            .Include(e => e.ExamRegistrations)
            .ThenInclude(er => er.Student)
            .FirstOrDefaultAsync(e => e.Id == guid);
        if (exam == null)
        {
            throw new Exception($"Exam with id {guid} not found");
        }
        _context.Exams.Remove(exam);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> UpdateExam(Guid guid, Exam exam)
    {
        var existingExam = _context.Exams
            .Include(e => e.Course)
            .FirstOrDefault(e => e.Id == guid);
        if (existingExam == null)
        {
            throw new Exception($"Exam with id {guid} not found");
        }
        existingExam.Date = exam.Date;
        existingExam.Location = exam.Location;
        existingExam.MaxScore = exam.MaxScore;
        existingExam.Type = exam.Type;
        await _context.SaveChangesAsync();
        return true;
    }

    public Task<List<Exam>> GetExamsByCourseId(Guid courseId)
    {
        var exams = _context.Exams
            .Include(e => e.Course)
            .Include(e => e.ExamRegistrations)
            .ThenInclude(er => er.Student)
            .Where(e => e.CourseId == courseId)
            .ToListAsync();
        return exams;
    }

    public Task<List<Exam>> GetExamsByTeacherId(string teacherId)
    {
        var exams = _context.Exams
            .Include(e => e.Course)
            .Include(e => e.ExamRegistrations)
            .ThenInclude(er => er.Student)
            .Where(e => e.Course.TeacherNeptunCode == teacherId)
            .ToListAsync();
        return exams;
    }
}