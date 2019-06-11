using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
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

        public IEnumerable<BookCategoryDto> GetAll()
        {
            return uow.BookCategorys.GetAll().Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public BookCategoryDto GetById(int id)
        {
            var entity = uow.BookCategorys.GetById(id);
            return new BookCategoryDto
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public bool Add(BookCategoryDto entity)
        {
            var newBookCategory = new BookCategory
            {
                Name = entity.Name
            };

            uow.BookCategorys.Add(newBookCategory);
            return uow.Complete();
        }

        public bool Update(int id, BookCategoryDto entity)
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
