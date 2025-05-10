namespace NeptunBackend.Models.DTO;

public class UpdateStudentDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}