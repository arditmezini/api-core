using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IBookCategoryRepository
    {
        Task<IEnumerable<BookCategory>> GetAll();
        Task<BookCategory> GetById(int id);
        Task Add(BookCategory bookCategory);
        void Update(BookCategory bookCategory);
        Task Delete(int id);
    }
}
