namespace PersonelApp.WebAPI.Services;

public interface IAuthTokenService
{
    string Create(Guid userId);
    bool CheckSecretKey(string secretKey);
}