using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreApi.Service
{
    public class BookCategoryService : IBookCategoryService
    {
        private readonly IUnitOfWork<ApiContext> uow;

        public BookCategoryService(IUnitOfWork<ApiContext> uow)
        {
            this.uow = uow;
        }

        public IEnumerable<BookCategoryDto> Get()
        {
            return uow.GetRepository<BookCategory>().Get().Select(x => new BookCategoryDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public BookCategoryDto GetById(int id)
        {
            return uow.GetRepository<BookCategory>()
                .FirstOrDefault(entity => new BookCategoryDto
                {
                    Id = entity.Id,
                    Name = entity.Name
                }, x => x.Id == id);
        }

        public void Add(BookCategoryDto entity)
        {
            var newBookCategory = new BookCategory
            {
                Name = entity.Name
            };

            uow.GetRepository<BookCategory>().Add(newBookCategory);
            uow.SaveChanges();
        }

        public void Update(int id, BookCategoryDto entity)
        {
            var oldEntity = uow.GetRepository<BookCategory>().FirstOrDefault(
                x => new BookCategory { Id = x.Id, Name = x.Name, DateCreated = x.DateCreated, UserCreated = x.UserCreated }, x => x.Id == id);

            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.Name = entity.Name;

            uow.GetRepository<BookCategory>().Update(oldEntity);
            uow.SaveChanges();
        }

        public void Delete(int id)
        {
            var oldEntity = uow.GetRepository<BookCategory>().FirstOrDefault(
                x => new BookCategory { Id = x.Id, Name = x.Name, DateCreated = x.DateCreated, UserCreated = x.UserCreated }, x => x.Id == id);
            if (oldEntity == null)
                throw new Exception("Entity to delete not found");

            oldEntity.IsDeleted = true;

            uow.GetRepository<BookCategory>().Update(oldEntity);
            uow.SaveChanges();
        }
    }
}
