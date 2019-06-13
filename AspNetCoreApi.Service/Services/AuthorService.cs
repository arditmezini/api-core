using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
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

        public IEnumerable<Author> Get()
        {
            return uow.Authors.GetAuthors().ToList();
        }
    }
}