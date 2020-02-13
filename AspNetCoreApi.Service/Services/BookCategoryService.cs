using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork uow;

        public BookCategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<IEnumerable<BookCategory>> GetAll()
        {
            return await uow.BookCategorys.GetAll();
        }

        public async Task<BookCategory> GetById(int id)
        {
            return await uow.BookCategorys.GetById(id);
        }

        public async Task<bool> Add(BookCategory entity)
        {
            await uow.BookCategorys.Add(entity);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Update(int id, BookCategory entity)
        {
            var oldEntity = await uow.BookCategorys.GetById(id);
            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.Name = entity.Name;

            uow.BookCategorys.Update(oldEntity);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            await uow.BookCategorys.Delete(id);
            return await uow.CompleteAsync();
        }
    }
}