using NeptunBackend.Models;
using NeptunBackend.Models.DTO;

namespace NeptunBackend.Services.Interfaces;

public interface IExamService
{
    Task<Exam?> GetExamById(Guid guid);
    Task<List<Exam?>> GetAllExams();
    Task<Exam> AddExam(CreateExamDTO exam);
    Task<bool> DeleteExam(Guid guid);
    Task<bool> UpdateExam(Guid guid, Exam exam);
    Task<List<Exam>> GetExamsByCourseId(Guid courseId);
    Task<List<Exam>> GetExamsByTeacherId(string teacherId);
}