using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetById(int id);
        Task<bool> Add(Book book);
        Task<bool> Update(int id, Book book);
        Task<bool> Delete(int id);
    }
}