namespace NeptunBackend.Models.DTO;

public class LoginDetailsDTO
{
    public string NeptunCode { get; set; }
    public string Password { get; set; }
    
    public LoginDetailsDTO(string neptunCode, string password)
    {
        NeptunCode = neptunCode;
        Password = password;
    }
    
    public LoginDetailsDTO() { }
}