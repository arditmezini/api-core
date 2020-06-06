using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Common.Identity;
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
    [Authorize(Policy = Role.User)]
    [Route("api/book")]
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

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Book with {id} retrived",
                mapper.Map<BookDto>(await bookService.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]BookDto entity)
        {
            return new ApiResponse("New book added.",
                await bookService.Add(mapper.Map<Book>(entity)));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]BookDto entity)
        {
            return new ApiResponse($"The record with {id} was updated.",
                await bookService.Update(id, mapper.Map<Book>(entity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return new ApiResponse($"The record with {id} was deleted.",
                await bookService.Delete(id));
        }
    }
}