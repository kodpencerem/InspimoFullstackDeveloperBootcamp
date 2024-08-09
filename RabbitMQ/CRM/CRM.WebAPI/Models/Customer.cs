namespace CRM.WebAPI.Models;

public sealed class Customer
{
    public Customer()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}
