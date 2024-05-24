using Microsoft.EntityFrameworkCore;
using PersonelApp.WebAPI.Models;

namespace PersonelApp.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("MyDb");
    }

    public DbSet<Personel> Personels { get; set; }
    public DbSet<User> Users { get; set; }
}
