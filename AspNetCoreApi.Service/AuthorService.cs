using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
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
                FirstName = x.FirstName,
                LastName = x.LastName,
                AuthorContact = new AuthorContactDto
                {
                    AuthorId = x.AuthorContact.AuthorId,
                    Address = x.AuthorContact.Address,
                    ContactNumber = x.AuthorContact.ContactNumber
                }
            }).ToList();
        }
    }
}
