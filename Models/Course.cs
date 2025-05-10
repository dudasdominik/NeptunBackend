using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NeptunBackend.Models;

public class Course
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Credits { get; set; }
    public string Semester { get; set; }
    public Teacher Teacher { get; set; }
    public string TeacherNeptunCode { get; set; }
    public List<Student> Students { get; set; } = new List<Student>();
    public List<Exam> Exams { get; set; } = new();

    
    public Course(string name, string description, int credits, string semester, Teacher teacher)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Credits = credits;
        Semester = semester;
        Teacher = teacher;
    }
    
    public Course() { }
    
    public void AddStudent(Student student)
    {
        Students.Add(student);
    }
    
    public void AddStudents(List<Student> students)
    {
        Students.AddRange(students);
    }
    
    public void RemoveStudent(Student student)
    {
        Students.Remove(student);
    }
    
    public void RemoveStudents(List<Student> students)
    {
        foreach (var student in students)
        {
            Students.Remove(student);
        }
    }
    
    public void UpdateCourse(string name, string description, int credits, string semester, Teacher teacher)
    {
        Name = name;
        Description = description;
        Credits = credits;
        Semester = semester;
        Teacher = teacher;
    }
    
}