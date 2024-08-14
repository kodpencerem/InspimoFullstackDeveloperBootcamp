using Bogus;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();

builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Clear();
    options.Providers.Add<GzipCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest; // Sýkýþtýrma seviyesi        
});

builder.Services.Configure<BrotliCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});

var app = builder.Build();

app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

app.UseResponseCompression();

app.MapGet("/", () => Employee.Employees.ToList());

seedData();
static void seedData()
{
    Console.WriteLine("Seed data is working...");
    for (int i = 0; i < 5000; i++)
    {
        Faker faker = new();
        Employee employee = new()
        {
            FirstName = faker.Person.FirstName,
            LastName = faker.Person.LastName,
            DateOfBirth = DateOnly.FromDateTime(faker.Person.DateOfBirth),
            AvatarUrl = faker.Person.Avatar,
            Salary = faker.Random.Decimal(17002, 100000),
            StartingDate = new DateOnly(2024, 08, 14)
        };

        Employee.Employees.Add(employee);
    }

    Console.WriteLine("Seed data is finish...");
}

app.Run();

class Employee
{
    public static List<Employee> Employees = new();
    public Employee()
    {
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public string AvatarUrl { get; set; } = default!;
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateOnly DateOfBirth { get; set; }
    public DateOnly StartingDate { get; set; }
    public decimal Salary { get; set; }

}