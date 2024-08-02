using eCommerce.Application.Services;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace eCommerce.Infrastructure.Services;
internal sealed class JwtProvider(
    IOptions<Jwt> options) : IJwtProvider
{
    public string CreateToken(User user)
    {
        List<string> roles = new() { "Products.Create", "Products.Update" };

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim("UserName",user.UserName),
            new Claim("Email",user.Email),
            new Claim("Fullname",user.FullName),
            new Claim(ClaimTypes.Role, JsonSerializer.Serialize(roles))
        };

        string secretKey = options.Value.SecretKey;

        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(secretKey));

        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);


        DateTime expires = DateTime.Now.AddDays(1);

        JwtSecurityToken jwtSecurityToken = new(
            issuer: options.Value.Issuer, //kim üretti
            audience: options.Value.Audience, //kimler kullanacak
            claims: claims,
            notBefore: DateTime.Now,
            expires: expires,
            signingCredentials: signingCredentials);

        JwtSecurityTokenHandler handler = new();

        string token = handler.WriteToken(jwtSecurityToken);

        return token;
    }
}
