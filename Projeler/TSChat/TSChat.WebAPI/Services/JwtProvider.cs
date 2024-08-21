using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Options;

namespace TSChat.WebAPI.Services;

public sealed class JwtProvider(IOptions<JwtOptions> options)
{
    public string CreatToken(User user)
    {
        List<Claim> claims = new()
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("UserName", user.UserName),
            new Claim("Avatar", user.Avatar),
            new Claim("FullName", user.FullName),
            new Claim("Profession",user.Profession)
        };

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

        DateTime expires = DateTime.Now.AddDays(1);

        JwtSecurityToken securityToken = new(
            issuer: options.Value.Issuer,
            audience: options.Value.Audience,
            signingCredentials: signingCredentials,
            claims: claims,
            expires: expires);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(securityToken);

        return token;
    }
}
