namespace NeptunBackend.Models.DTO;

public class CreateStudentDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    
    public CreateStudentDTO(string firstName, string lastName, DateTime birthDate, string phoneNumber, string address, string email, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        PhoneNumber = phoneNumber;
        Address = address;
        Email = email;
        Password = password;
    }

   

    public Student ToStudent()
    {
        return new Student(FirstName, LastName, Email, PhoneNumber, Address,  Password, BirthDate, State.Active);
    }
}