using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
{
    public class BookCategoryMapping : Profile
    {
        public BookCategoryMapping()
        {
            CreateMap<BookCategory, BookCategoryDto>()
                .ReverseMap();
        }
    }
}