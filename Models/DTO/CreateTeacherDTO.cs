namespace NeptunBackend.Models.DTO;

public class CreateTeacherDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? University { get; set; }
    public string? Department { get; set; }


    public CreateTeacherDTO(string? firstName, string? lastName, DateTime birthDate, string? phoneNumber, string? address, string? email, string? password, string? university, string? department)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
        Password = password;
        University = university;
        Department = department;
    }
    
    public Teacher ToTeacher()
    {
        return new Teacher(FirstName, LastName, Email, PhoneNumber, Address, Password, BirthDate, University, Department);
    }
}