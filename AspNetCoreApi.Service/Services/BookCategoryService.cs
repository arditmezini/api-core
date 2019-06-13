using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service.Services
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork uow;

        public BookCategoryService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BookCategory> GetAll()
        {
            return uow.BookCategorys.GetAll().ToList();
        }

        public BookCategory GetById(int id)
        {
            return uow.BookCategorys.GetById(id);
        }

        public bool Add(BookCategory entity)
        {
            uow.BookCategorys.Add(entity);
            return uow.Complete();
        }

        public bool Update(int id, BookCategory entity)
        {
            var oldEntity = uow.BookCategorys.GetById(id);
            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.Name = entity.Name;

            uow.BookCategorys.Update(oldEntity);
            return uow.Complete();
        }

        public bool Delete(int id)
        {
            uow.BookCategorys.Delete(id);
            return uow.Complete();
        }
    }
}