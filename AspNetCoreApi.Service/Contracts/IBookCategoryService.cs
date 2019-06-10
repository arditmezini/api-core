using AspNetCoreApi.Models.Dto;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookCategoryService
    {
        IEnumerable<BookCategoryDto> Get();
        BookCategoryDto GetById(int id);
        void Add(BookCategoryDto entity);
        void Update(int id, BookCategoryDto entity);
        void Delete(int id);
    }
}
