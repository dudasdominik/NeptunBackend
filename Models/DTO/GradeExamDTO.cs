namespace NeptunBackend.Models.DTO;

public class GradeExamDTO
{
    public int Grade { get; set; }

    public GradeExamDTO(int grade)
    {
        Grade = grade;
    }
}