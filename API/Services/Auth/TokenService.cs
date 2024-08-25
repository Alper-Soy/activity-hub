using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace API.Services.Auth;

public class TokenService(IConfiguration config)
{
    public string CreateToken(AppUser appUser)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, appUser.UserName),
            new(ClaimTypes.NameIdentifier, appUser.Id),
            new(ClaimTypes.Email, appUser.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}
