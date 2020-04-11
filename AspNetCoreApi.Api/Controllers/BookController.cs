using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Roles = Role.User)]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService, IMapper mapper)
            : base(mapper)
        {
            this.bookService = bookService ?? throw new ArgumentNullException(nameof(bookService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            return new ApiResponse("Books retrived",
                mapper.Map<IEnumerable<BookDto>>(await bookService.GetAll()));
        }
    }
}