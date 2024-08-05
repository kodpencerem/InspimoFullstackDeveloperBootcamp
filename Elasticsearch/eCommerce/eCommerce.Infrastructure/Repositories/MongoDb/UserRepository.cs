using eCommerce.Domain.Entities;
using eCommerce.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace eCommerce.Infrastructure.Repositories.MongoDb;
internal sealed class UserRepository : IUserRepository
{
    private readonly IMongoCollection<User> _users;

    public UserRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
        var database = client.GetDatabase(configuration.GetSection("MongoDbOptions:DbName").Value);
        _users = database.GetCollection<User>("users");
    }
    public async Task<Guid> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        InsertOneOptions insertOneOptions = new();
        await _users.InsertOneAsync(user, insertOneOptions, cancellationToken);

        return user.Id;
    }

    public async Task<User?> FindByUserNameAndPasswordAsync(string userName, string password, CancellationToken cancellationToken = default)
    {
        return await _users.Find<User>(p => p.UserName == userName && p.Password == password).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsEmailExistsAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _users.Find<User>(p => p.Email == email).AnyAsync(cancellationToken);
    }

    public async Task<bool> IsUserNameExistsAsync(string userName, CancellationToken cancellationToken = default)
    {
        return await _users.Find<User>(p => p.UserName == userName).AnyAsync(cancellationToken);
    }
}
