using Microsoft.EntityFrameworkCore;
using TodoApp.WebAPI.Models;

namespace TodoApp.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDb");
    }

    public DbSet<Todo> Todos { get; set; }
}
