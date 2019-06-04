using AspNetCoreApi.Models.Dto;
using System.Collections.Generic;

namespace AspNetCoreApi.Contracts
{
    public interface IAuthorService
    {
        IEnumerable<AuthorDto> Get();
    }
}
