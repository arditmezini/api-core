using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookCategoryService
    {
        Task<IEnumerable<BookCategory>> GetAll();
        Task<BookCategory> GetById(int id);
        Task<bool> Add(BookCategory entity);
        Task<bool> Update(int id, BookCategory entity);
        Task<bool> Delete(int id);
    }
}