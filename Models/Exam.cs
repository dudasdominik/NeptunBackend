namespace NeptunBackend.Models;

public class Exam
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Location { get; set; }

    public DateTime Date { get; set; }

    public string Type { get; set; }

    public int MaxScore { get; set; }
    
    public Course Course { get; set; }
    public Guid CourseId { get; set; }

    
    public List<ExamRegistration> ExamRegistrations { get; set; } = new();

    public Exam(DateTime date, string type, int maxScore, Course course, string location)
    {
        Date = date;
        Type = type;
        MaxScore = maxScore;
        Course = course;
        Location = location;
    }
    public Exam() { }
}
