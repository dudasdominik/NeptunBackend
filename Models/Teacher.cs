namespace NeptunBackend.Models;

public class Teacher : Person
{
    public Teacher(string firstName, string lastName, string email, string phone, string address, string password, DateTime birthDate, string university, string department) : base(firstName, lastName, email, phone, address, password, birthDate)
    {
        University = university;
        Department = department;
    }

    public Teacher(List<Course> teacherCourses, string university, string department)
    {
        TeacherCourses = teacherCourses;
        University = university;
        Department = department;
    }
    public Teacher() { }

    public List<Course> TeacherCourses { get; set; } = new List<Course>();
    
    public string University { get; set; }
    
    public string Department { get; set; }
    
    public void AddCourse(Course course)
    {
        TeacherCourses.Add(course);
    }
    public void AddCourses(List<Course> courses)
    {
        TeacherCourses.AddRange(courses);
    }
    public void RemoveCourse(Course course)
    {
        TeacherCourses.Remove(course);
    }
    public void RemoveCourses(List<Course> courses)
    {
        foreach (var course in courses)
        {
            TeacherCourses.Remove(course);
        }
    }
}