using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using NeptunBackend.Models;

namespace NeptunBackend.Services;

public class TokenService
{
    public string GenerateToken(Person user)
    {
        var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key");
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new Exception("JWT key is not set in the environment variables.");
        }
        var key = Encoding.UTF8.GetBytes(jwtKey);
        
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.NeptunCode),
            new Claim(ClaimTypes.Role, user.GetType().Name)
        };
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}