using PersonelApp.WebAPI.Models;
using PersonelApp.WebAPI.Repositories;
using System.Text;

namespace PersonelApp.WebAPI.Services;

public sealed class AuthTokenService(
    IAuthTokenRepository authTokenRepository) : IAuthTokenService
{
    public bool CheckSecretKey(string secretKey)
    {
        AuthToken? authToken = authTokenRepository.CheckSecretkeyIsAvailable(secretKey);

        if (authToken is null || authToken.ExpireDate < DateTime.Now)
        {
            return false;
        }

        return true;
    }

    public string Create(Guid userId, bool rememberMe = false)
    {
        byte[] secretKey;

        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            long time = DateTime.Now.ToFileTime();
            secretKey =
                hmac
                .ComputeHash(
                    Encoding.UTF8.GetBytes(time.ToString() + "My secret key" + userId.ToString()));
        }

        DateTime expires = DateTime.Now.AddDays(1);
        if (rememberMe)
        {
            expires = DateTime.Now.AddMonths(1);
        }

        AuthToken authToken = new()
        {
            UserId = userId,
            CreateDate = DateTime.Now,
            ExpireDate = expires,
            SecretKey = Convert.ToBase64String(secretKey)
        };

        var result = authTokenRepository.Create(authToken);
        if (result)
        {
            return authToken.SecretKey;
        }

        throw new ArgumentException("Something went wrong");
    }
}
