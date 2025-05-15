using Microsoft.AspNetCore.Mvc;
using NeptunBackend.Models;
using NeptunBackend.Models.DTO;

namespace NeptunBackend.Services.Interfaces;

public interface IStudentService
{
    Task<Student> GetStudentByNeptunCode(string neptunCode);
    Task<List<Student?>> GetAllStudents();
    Task<Student> AddStudent(CreateStudentDTO student);
    Task<Student> GetStudentByEmail(string email);
    Task<Student> LogInStudent(string neptunCode, string password);
    Task<Student> UpdateStudent(string neptunCode, UpdateStudentDTO student);
    Task<bool> DeleteStudent(string neptunCode);
    Task<Student> UpdateStudentPassword(string neptunCode, string currentPassword, string newPassword);
    Task<Student> EnrollCourse(string neptunCode, Guid courseId);
    Task<string> LogIn(string neptunCode, string password);
    Task<ExamRegistration> RegisterForExam(string neptunCode, Guid examId);
    Task<List<Course>> GetAllCourses(string neptunCode);
}