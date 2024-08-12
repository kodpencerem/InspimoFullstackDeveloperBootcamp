namespace Example.WebAPI.DTOs;

public sealed record CreateCustomerDto(
    string FirstName,
    string LastName,
    string Email
    );
