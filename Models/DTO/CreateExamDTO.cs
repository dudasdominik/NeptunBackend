namespace NeptunBackend.Models.DTO;

public class CreateExamDTO
{
    public string Location { get; set; }
    public DateTime Date { get; set; }
    public string Type { get; set; }
    public int MaxScore { get; set; }
    public Guid CourseId { get; set; }

    public Exam ToExam(Course course)
    {
        return new Exam
        {
            Location = Location,
            Date = Date,
            Type = Type,
            MaxScore = MaxScore,
            Course = course,
            CourseId = course.Id
        };
    }

    
}