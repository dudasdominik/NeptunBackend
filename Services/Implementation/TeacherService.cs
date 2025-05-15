using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class TeacherService : NeptunService, ITeacherService
{ 
    
    private readonly TokenService _tokenService;

    public TeacherService(NeptunDbContext context, TokenService tokenService) : base (context)
    {
        _tokenService = tokenService;
    }

    public async Task<Teacher?> GetTeacherByNeptunCode(string neptunCode)
    {
        return await _context.Teachers
            .FirstOrDefaultAsync(t => t.NeptunCode == neptunCode);
    }

    public Task<List<Teacher?>> GetAllTeachers()
    {
        return _context.Teachers.ToListAsync();
    }

    public async Task<Teacher> AddTeacher(CreateTeacherDTO teacher)
    {
        IPasswordHasher<Teacher> passwordHasher = new PasswordHasher<Teacher>();
        var convertedTeacher = teacher.ToTeacher();
        convertedTeacher.Password = passwordHasher.HashPassword(convertedTeacher, convertedTeacher.Password);

        _context.Teachers.Add(convertedTeacher);
        await _context.SaveChangesAsync();
        return convertedTeacher;
    }

    public async Task<Exam> CreateExamForCourse(CreateExamDTO createExam, Guid courseId, string neptunCode)
    {
        var teacher = await GetTeacherByNeptunCode(neptunCode);
        if (teacher == null)
        {
            throw new Exception($"Teacher with neptun code {neptunCode} not found");
        }
        var a = await GetAllCourses(teacher.NeptunCode);
        var course = a.FirstOrDefault(c => c.Id == courseId);
        if (a.Contains(course))
        {
            var exam = createExam.ToExam(course);
            _context.Exams.Add(exam);
            await _context.SaveChangesAsync();
            return exam;
        };
        throw new Exception($"Course with id {courseId} not found");
    }

    public async Task<List<Course>> GetAllCourses(string neptunCode)
    {
        return await _context.Courses
            .Include(c => c.Exams)
            .Include(c => c.Students)
            .Where(c => c.TeacherNeptunCode == neptunCode)
            .ToListAsync();
    }
    public async Task<string> LogIn(string neptunCode, string password)
    {
        var foundTeacher = await GetTeacherByNeptunCode(neptunCode);
        if (foundTeacher == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        IPasswordHasher<Teacher> passwordHasher = new PasswordHasher<Teacher>();
        var result = passwordHasher.VerifyHashedPassword(foundTeacher, foundTeacher.Password, password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception($"Password is incorrect");
        }
        var token = _tokenService.GenerateToken(foundTeacher);
        return token;
    }
}