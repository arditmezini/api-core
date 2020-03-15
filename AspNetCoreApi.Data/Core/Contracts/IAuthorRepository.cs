using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Dal.Core.Contracts
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetById(int id);
        Task Add(Author author);
        void Update(Author author);
        Task Delete(int id);
    }
}
