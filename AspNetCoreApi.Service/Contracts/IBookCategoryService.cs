using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookCategoryService
    {
        IEnumerable<BookCategory> GetAll();
        BookCategory GetById(int id);
        bool Add(BookCategory entity);
        bool Update(int id, BookCategory entity);
        bool Delete(int id);
    }
}
