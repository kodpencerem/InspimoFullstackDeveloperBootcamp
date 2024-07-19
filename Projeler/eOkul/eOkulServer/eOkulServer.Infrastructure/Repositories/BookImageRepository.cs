using eOkulServer.Domain.Entities;
using eOkulServer.Infrastructure.Context;

namespace eOkulServer.Infrastructure.Repositories;

internal sealed class BookImageRepository : Repository<BookImage>
{
    public BookImageRepository(ApplicationDbContext context) : base(context)
    {
    }
}