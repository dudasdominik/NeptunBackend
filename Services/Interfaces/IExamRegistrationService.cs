using NeptunBackend.Models;

namespace NeptunBackend.Services.Interfaces;

public interface IExamRegistrationService
{
    Task<ExamRegistration?> GetExamRegistrationById(Guid guid);
    Task<List<ExamRegistration?>> GetAllExamRegistrations();
    Task<ExamRegistration> AddExamRegistration(ExamRegistration examRegistration);
    Task<bool> DeleteExamRegistration(Guid guid);
    Task<bool> UpdateExamRegistration(Guid guid, ExamRegistration examRegistration);
    Task<List<ExamRegistration>> GetExamRegistrationsByStudentId(string studentId);
    Task<List<ExamRegistration>> GetExamRegistrationsByExamId(Guid examId);
    Task<List<ExamRegistration>> GetExamRegistrationsByCourseId(Guid courseId);
    Task<List<ExamRegistration>> GetExamRegistrationsByTeacherId(string teacherId);
    
}