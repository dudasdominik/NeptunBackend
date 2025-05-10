using System.ComponentModel.DataAnnotations.Schema;

namespace NeptunBackend.Models;

[Table("Students")]
public class Student : Person
{
    public State State { get; set; }

    public virtual List<Course> Courses { get; set; } = new List<Course>();
    public List<ExamRegistration> ExamRegistrations { get; set; } = new();

    public Student(string firstName, string lastName, string email, string phone, string address, string password, DateTime birthDate, State state) : base(firstName, lastName, email, phone, address, password, birthDate)
    {
        State = state;
    }

    public Student() { }

    public string GetState()
    {
        return State.ToString();
    }

    public void AddCourse(Course course)
    {
        Courses.Add(course);
    }

    public void AddCourses(List<Course> courses)
    {
        Courses.AddRange(courses);
    }

    public void RemoveCourse(Course course)
    {
        Courses.Remove(course);
    }
    public void RemoveCourses(List<Course> courses)
    {
        foreach (var course in courses)
        {
            Courses.Remove(course);
        }
    }
}