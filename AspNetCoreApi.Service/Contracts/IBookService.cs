using AspNetCoreApi.Models.Common.Paging;
using AspNetCoreApi.Models.Entity;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookService
    {
        Task<PagedList<Book>> GetAll(PagedParams bookParams);
        Task<Book> GetById(int id);
        Task<bool> Add(Book book);
        Task<bool> Update(int id, Book book);
        Task<bool> Delete(int id);
    }
}