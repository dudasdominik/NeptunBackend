namespace NeptunBackend.Models;

public class Administrator : Person
{
    public Administrator(string firstName, string lastName, string email, string phone, string address, string password, DateTime birthDate) : base(firstName, lastName, email, phone, address, password, birthDate)
    {
    }

    public Administrator() { }
}