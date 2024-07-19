using eOkulServer.Domain.Entities;
using eOkulServer.Infrastructure.Context;

namespace eOkulServer.Infrastructure.Repositories;

internal sealed class BookCategoryRepository : Repository<BookCategory>
{
    public BookCategoryRepository(ApplicationDbContext context) : base(context)
    {
    }
}
