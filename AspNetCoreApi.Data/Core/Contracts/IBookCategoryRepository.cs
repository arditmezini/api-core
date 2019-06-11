using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IBookCategoryRepository
    {
        IEnumerable<BookCategory> GetAll();
        BookCategory GetById(int id);
        void Add(BookCategory bookCategory);
        void Update(BookCategory bookCategory);
        void Delete(int id);
    }
}
