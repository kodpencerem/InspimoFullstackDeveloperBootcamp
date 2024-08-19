using System.Text;

namespace TSChat.WebAPI.Services;

public sealed class HashingHelper
{
    public (byte[] passwordHash, byte[] passwordSalt) CreatePassword(string password)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512();
        byte[] passwordSalt = hmac.Key;
        byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return (passwordHash, passwordSalt);
    }

    public bool VerifyPasswordHash(string password, byte[] passwordSalt, byte[] passwordHash)
    {
        using var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt);

        var computeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        for (int i = 0; i < computeHash.Length; i++)
        {
            if (computeHash[i] != passwordHash[i]) return false;
        }

        return true;
    }
}
