using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
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