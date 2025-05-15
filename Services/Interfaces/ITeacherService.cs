using NeptunBackend.Models;
using NeptunBackend.Models.DTO;

namespace NeptunBackend.Services.Interfaces;

public interface ITeacherService
{
    Task<Teacher?> GetTeacherByNeptunCode(string neptunCode);
    Task<List<Teacher?>> GetAllTeachers();
    Task<Teacher> AddTeacher(CreateTeacherDTO teacher);
    Task<Exam> CreateExamForCourse(CreateExamDTO createExam, Guid courseId, string neptunCode);
    Task<List<Course>> GetAllCourses(string neptunCode);
    Task<string> LogIn(string neptunCode, string password);
}