using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreAPI.Infraestructure.EntityFramework.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using StoreAPI.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using StoreAPI.Infraestructure.EntityFramework.Daos;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _context;

    public AuthService(IConfiguration config, ApplicationDbContext context)
    {
        _config = config;
        _context = context;
    }

    //public async Task<string> AuthenticateAsync(string email, string password)
    //{
    //    var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

    //    if (user == null || !VerifyPassword(password, user.PasswordHash))
    //        return null;

    //    return GenerateJwtToken(user);
    //}

    private bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            return BCrypt.Net.BCrypt.Verify(password, storedHash);
        }
        catch
        {
            return false;
        }
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}