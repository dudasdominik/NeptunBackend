using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeptunBackend.Data;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;
using NeptunBackend.Services.Interfaces;

namespace NeptunBackend.Services.Implementation;

public class StudentService : NeptunService, IStudentService
{
    private readonly TokenService _tokenService;
    public StudentService(NeptunDbContext context, TokenService service) : base (context)
    {
       _tokenService = service;
    }
    public async Task<Student> GetStudentByNeptunCode(string neptunCode)
    {
        return await _context.Students
            .FirstOrDefaultAsync(s => s.NeptunCode == neptunCode);
    }

    public Task<List<Student?>> GetAllStudents()
    {
        return _context.Students.ToListAsync();
    }

    public async Task<Student> AddStudent(CreateStudentDTO student)
    {
        IPasswordHasher<Student> passwordHasher = new PasswordHasher<Student>();
        var convertedStudent = student.ToStudent();
        convertedStudent.Password = passwordHasher.HashPassword(convertedStudent, convertedStudent.Password);

        _context.Students.Add(convertedStudent);
        await _context.SaveChangesAsync();
        return convertedStudent;
    }

    public Task<Student> GetStudentByEmail(string email)
    {
        return _context.Students
            .FirstOrDefaultAsync(s => s.Email == email);
    }

    public async Task<Student> LogInStudent(string neptunCode, string password)
    {
        var student = await GetStudentByNeptunCode(neptunCode);
        if (student == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        IPasswordHasher<Student> passwordHasher = new PasswordHasher<Student>();
        var result = passwordHasher.VerifyHashedPassword(student, student.Password, password);
        if (result == PasswordVerificationResult.Success)
        {
            return student;
        }
        else
        {
            throw new Exception($"Password is incorrect");
        }
    }

    public async Task<Student> UpdateStudent(string neptunCode, UpdateStudentDTO student)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        foundStudent.FirstName = student.FirstName;
        foundStudent.LastName = student.LastName;
        foundStudent.Address = student.Address;
        foundStudent.BirthDate = student.BirthDate;
        foundStudent.Phone = student.PhoneNumber;
        
        await _context.SaveChangesAsync();
        return foundStudent;
        
    }

    public async Task<bool> DeleteStudent(string neptunCode)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        var response = _context.Students.Remove(foundStudent);
        await _context.SaveChangesAsync();
        return !response.Equals(null);
    }

    public async Task<Student> UpdateStudentPassword(string neptunCode, string currentPassword, string newPassword)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        IPasswordHasher<Student> passwordHasher = new PasswordHasher<Student>();
        var result = passwordHasher.VerifyHashedPassword(foundStudent, foundStudent.Password, currentPassword);
        if (result == PasswordVerificationResult.Success)
        {
            foundStudent.Password = passwordHasher.HashPassword(foundStudent, newPassword);
            await _context.SaveChangesAsync();
            return foundStudent;
        }
        else
        {
            throw new Exception($"Password is incorrect");
        }
    }

    public async Task<Student> EnrollCourse(string neptunCode, Guid courseId)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        var foundCourse = await _context.Courses.FirstOrDefaultAsync(c => c.Id == courseId);
        if (foundCourse == null)
        {
            throw new Exception($"Course with id {courseId} not found");
        }
        foundStudent.AddCourse(foundCourse);
        foundCourse.AddStudent(foundStudent);
        _context.Courses.Update(foundCourse);
        _context.Students.Update(foundStudent);
        await _context.SaveChangesAsync();
        return foundStudent;
    }

    public async Task<string> LogIn(string neptunCode, string password)
    {
        var foundStudent = await GetStudentByNeptunCode(neptunCode);
        if (foundStudent == null)
        {
            throw new Exception($"Student with neptun code {neptunCode} not found");
        }
        IPasswordHasher<Student> passwordHasher = new PasswordHasher<Student>();
        var result = passwordHasher.VerifyHashedPassword(foundStudent, foundStudent.Password, password);
        if (result != PasswordVerificationResult.Success)
        {
            throw new Exception($"Password is incorrect");
        }
        var token = _tokenService.GenerateToken(foundStudent);
        return token;
    }

    public async Task<ExamRegistration> RegisterForExam(string neptunCode, Guid examId)
    {
        var student = await _context.Students
            .Include(s => s.Courses)
            .ThenInclude(c => c.Exams)
            .FirstOrDefaultAsync(s => s.NeptunCode == neptunCode);

        if (student == null)
            throw new Exception($"Student with Neptun code {neptunCode} not found");

        var exam = await _context.Exams
            .Include(e => e.Course)
            .FirstOrDefaultAsync(e => e.Id == examId);

        if (exam == null)
            throw new Exception($"Exam with id {examId} not found");

        
        var isRegisteredToCourse = student.Courses.Any(c => c.Id == exam.Course.Id);

        if (!isRegisteredToCourse)
            throw new Exception("Student is not registered for the course associated with this exam");

        
        bool alreadyRegistered = await _context.ExamRegistrations
            .AnyAsync(er => er.StudentNeptunCode == neptunCode && er.ExamId == examId);

        if (alreadyRegistered)
            throw new Exception("Student is already registered for this exam");

        var registration = new ExamRegistration
        {
            StudentNeptunCode = student.NeptunCode,
            Student = student,
            ExamId = exam.Id,
            Exam = exam,
            Grade = null
        };

        _context.ExamRegistrations.Add(registration);
        await _context.SaveChangesAsync();

        return registration;
    }

    public async Task<List<Course>> GetAllCourses(string neptunCode)
    {
        return await _context.Courses
            .Include(c => c.Exams)
            .Include(c => c.Students)
            .Where(c => c.Students.Any(s => s.NeptunCode == neptunCode))
            .ToListAsync();
    }
}