using AspNetCoreApi.Contracts;
using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public AuthorService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<AuthorDto> Get()
        {
            return uow.GetRepository<Author>().Get().Select(x => new AuthorDto
            {
                Id = x.Id,
                Name = x.Name,
                AuthorContact = new AuthorContactDto
                {
                    Address = x.AuthorContact.Address,
                    ContactNumber = x.AuthorContact.ContactNumber
                }
            }).ToList();
        }
    }
}
