using AspNetCoreApi.Models.Common.Paging;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AutoMapper;

namespace AspNetCoreApi.Common.Mapping
{
    public class BookMapping : Profile
    {
        public BookMapping()
        {
            CreateMap<Book, BookDto>()
                .ReverseMap();

            CreateMap<PagedList<Book>, PagedList<BookDto>>()
                .ReverseMap();
        }
    }
}