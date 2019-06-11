using AspNetCoreApi.Models.Dto;
using System.Collections.Generic;

namespace AspNetCoreApi.Service.Contracts
{
    public interface IBookCategoryService
    {
        IEnumerable<BookCategoryDto> GetAll();
        BookCategoryDto GetById(int id);
        bool Add(BookCategoryDto entity);
        bool Update(int id, BookCategoryDto entity);
        bool Delete(int id);
    }
}
