namespace eCommerce.Application.Auth.Login;

public sealed record LoginCommandResponse(
    Guid UserId,
    string AccessToken);