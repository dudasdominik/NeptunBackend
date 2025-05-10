namespace NeptunBackend.Models;

public class ExamRegistration
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string StudentNeptunCode { get; set; }
    public Student Student { get; set; }

    public Guid ExamId { get; set; }
    public Exam Exam { get; set; }

    public int? Grade { get; set; } = null;

    public ExamRegistration(string studentNeptunCode, Student student, Guid examId, Exam exam, int? grade)
    {
        StudentNeptunCode = studentNeptunCode;
        Student = student;
        ExamId = examId;
        Exam = exam;
        Grade = grade;
    }
    public ExamRegistration() { }
}