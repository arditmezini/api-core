using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;

namespace AspNetCoreApi.Dal.Core
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApiContext context)
            : base(context)
        { }
    }
}