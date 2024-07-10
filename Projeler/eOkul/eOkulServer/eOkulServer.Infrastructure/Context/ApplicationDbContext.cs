using eOkulServer.Domain.Entities;
using eOkulServer.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eOkulServer.Infrastructure.Context;
internal sealed class ApplicationDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<UserType> UserTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Ignore<IdentityUserRole<Guid>>();
        builder.Ignore<IdentityUserClaim<Guid>>();
        builder.Ignore<IdentityRoleClaim<Guid>>();
        builder.Ignore<IdentityUserLogin<Guid>>();
        builder.Ignore<IdentityUserToken<Guid>>();

        builder.Entity<User>().OwnsOne(p => p.Address, builder =>
        {
            builder.Property(p => p.City).HasColumnName("City");
            builder.Property(p => p.Town).HasColumnName("Town");
            builder.Property(p => p.FullAddress).HasColumnName("FullAddress");
        });
    }
}
