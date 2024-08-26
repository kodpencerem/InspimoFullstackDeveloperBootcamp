using Microsoft.EntityFrameworkCore;
using TSChat.WebAPI.Models;

namespace TSChat.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Chat> Chats { get; set; }
}
