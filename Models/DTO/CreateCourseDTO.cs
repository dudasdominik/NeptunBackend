namespace NeptunBackend.Models.DTO;

public class CreateCourseDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }
    public string Semester { get; set; }
    public string TeacherNeptunCode { get; set; }

    public CreateCourseDTO(string name, string description, int credits, string semester, string teacherNeptunCode)
    {
        Name = name;
        Description = description;
        Credits = credits;
        Semester = semester;
        TeacherNeptunCode = teacherNeptunCode;
    }
    
    public Course ToCourse(Teacher teacher)
    {
        return new Course(Name, Description, Credits, Semester, teacher);
    }
}