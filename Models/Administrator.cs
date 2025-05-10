namespace NeptunBackend.Models;

public class Administrator : Person
{
    public AdminRole Role { get; set; }

    public Administrator(string firstName, string lastName, string email, string phone, string address, string password, DateTime birthDate, AdminRole role) : base(firstName, lastName, email, phone, address, password, birthDate)
    {
        Role = role;
    }
    public Administrator() { }
}