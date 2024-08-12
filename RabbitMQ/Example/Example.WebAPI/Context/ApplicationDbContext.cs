using Example.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Example.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Customer> Customers { get; set; }
}