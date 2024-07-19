using eOkulServer.Domain.Entities;
using eOkulServer.Infrastructure.Context;

namespace eOkulServer.Infrastructure.Repositories;
internal sealed class BookRepository : Repository<Book>
{
    public BookRepository(ApplicationDbContext context) : base(context)
    {
    }
}