using Microsoft.EntityFrameworkCore;
using TS.Result;
using TSChat.WebAPI.Context;
using TSChat.WebAPI.Models;
using TSChat.WebAPI.Services;

namespace TSChat.WebAPI.Repositories;

public sealed class UserEFCoreRepository(
    ApplicationDbContext context) : IUserRepository
{
    public async Task<Result<string>> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await context.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return "Create is successful";
    }

    public async Task<Result<List<User>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await context.Users.AsNoTracking().OrderBy(p => p.FirstName).ToListAsync(cancellationToken);
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.FindAsync(id, cancellationToken);
    }

    public async Task<User?> GetUserByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        User? user = await context.Users.FirstOrDefaultAsync(p => p.UserName == userName, cancellationToken);
        if (user is null) return null;


        bool verifyPassword = new HashingHelper().VerifyPasswordHash(password, user.PasswordSalt, user.PasswordHash);

        return !verifyPassword ? null : user;
    }

    public async Task<bool> IsUserNameExist(string userName, CancellationToken cancellationToken = default)
    {
        return await context.Users.AnyAsync(p => p.UserName == userName, cancellationToken);
    }

    public async Task<Result<string>> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        context.Update(user);
        await context.SaveChangesAsync(cancellationToken);

        return "Update is successful";
    }
}
