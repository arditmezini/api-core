using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AutoMapper;

namespace AspNetCoreApi.Api.Mapping
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, BookDto>()
                .ReverseMap();
        }
    }
}