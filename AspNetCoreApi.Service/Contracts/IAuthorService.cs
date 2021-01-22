using AspNetCoreApi.Models.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<Author>> Get();
        Task<Author> GetById(int id);
        Task<bool> Add(Author author);
        Task<bool> Update(int id, Author author);
        Task<bool> Delete(int id);
    }
}