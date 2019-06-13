using AspNetCoreApi.Dal.Entities;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IAuthorService
    {
        IEnumerable<Author> Get();
    }
}
