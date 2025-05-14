using System.ComponentModel.DataAnnotations;

namespace NeptunBackend.Models;


public abstract class Person
{
    [Key]
    [MaxLength(5)]
    [RegularExpression("^[A-Z0-9]{5}$", ErrorMessage = "Neptun code must be 5 uppercase alphanumeric characters.")]
    public string NeptunCode { get; set; }

    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }

    [Required]
    public string Phone { get; set; }

    [Required]
    public string Address { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public Role Role { get; set; } = new();

    
    protected Person(string firstName, string lastName, string email, string phone, string address, string password, DateTime birthDate)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Address = address;
        Password = password;
        BirthDate = birthDate;
        NeptunCode = GenerateNeptunCode();
    }

    protected Person() { }

    private static string GenerateNeptunCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Range(0, 5).Select(_ => chars[random.Next(chars.Length)]).ToArray());
    }
}