﻿using AspNetCoreApi.Dal.Core.Contracts;
using AspNetCoreApi.Models.Common.Paging;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AspNetCoreApi.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IUnitOfWork uow;

        public BookService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public async Task<PagedList<Book>> GetAll(PagedParams bookParams)
        {
            return await uow.Books.Get(include: x => x.Include(y => y.BookCategory).Include(y => y.Publisher), filter: null, orderBy: null, bookParams.PageNumber, bookParams.PageSize);
        }

        public async Task<Book> GetById(int id)
        {
            return await uow.Books.GetById(id);
        }

        public async Task<bool> Add(Book book)
        {
            await uow.Books.Add(book);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Update(int id, Book book)
        {
            var oldEntity = await uow.Books.GetById(id);
            if (oldEntity == null)
                throw new Exception("Entity to update not found");

            oldEntity.Title = book.Title;
            oldEntity.Isbn = book.Isbn;
            oldEntity.PublishedYear = book.PublishedYear;
            oldEntity.CategoryId = book.CategoryId;
            oldEntity.PublisherId = book.PublisherId;

            uow.Books.Update(oldEntity);
            return await uow.CompleteAsync();
        }

        public async Task<bool> Delete(int id)
        {
            await uow.Books.Delete(id);
            return await uow.CompleteAsync();
        }
    }
}