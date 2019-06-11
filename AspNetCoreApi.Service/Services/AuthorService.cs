using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork uow;

        public AuthorService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<AuthorDto> Get()
        {
            return uow.Authors.GetAuthors().Select(x => new AuthorDto
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
