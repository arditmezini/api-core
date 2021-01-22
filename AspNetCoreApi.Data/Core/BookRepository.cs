using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Entity;

namespace AspNetCoreApi.Dal.Core
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApiContext context)
            : base(context)
        { }
    }
}