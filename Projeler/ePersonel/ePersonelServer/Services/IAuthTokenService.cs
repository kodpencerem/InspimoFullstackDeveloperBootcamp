namespace PersonelApp.WebAPI.Services;

public interface IAuthTokenService
{
    string Create(Guid userId, bool rememberMe = false);
    bool CheckSecretKey(string secretKey);
}