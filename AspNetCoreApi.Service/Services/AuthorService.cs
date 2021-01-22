using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IUnitOfWork uow;

        public AuthorService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<Author>> Get()
        {
            return await uow.Authors.GetAuthors();
        }

        public async Task<Author> GetById(int id)
        {
            return await uow.Authors.GetById(id);
        }

        public async Task<bool> Add(Author author)
        {
            await uow.Authors.Add(author);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Update(int id, Author author)
        {
            var oldEntity = await uow.Authors.GetById(id);
            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.FirstName = author.FirstName;
            oldEntity.LastName = author.LastName;

            oldEntity.AuthorContact.CountryId = author.AuthorContact.CountryId;
            oldEntity.AuthorContact.ContactNumber = author.AuthorContact.ContactNumber;
            oldEntity.AuthorContact.Address = author.AuthorContact.Address;

            uow.Authors.Update(oldEntity);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            await uow.Authors.Delete(id);
            return await uow.CompleteAsync();
        }
    }
}