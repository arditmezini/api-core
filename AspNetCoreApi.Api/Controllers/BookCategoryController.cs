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
    [ApiController]
    [Authorize(Policy = Role.Manager)]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/book-category")]
    public class BookCategoryController : BaseController
    {
        private readonly IBookCategoryService bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService, IMapper mapper)
            : base(mapper)
        {
            this.bookCategoryService = bookCategoryService ?? throw new ArgumentNullException(nameof(bookCategoryService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            return new ApiResponse("Book Categories retrived",
                mapper.Map<IEnumerable<BookCategoryDto>>(await bookCategoryService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Book category with {id} retrived.",
                mapper.Map<BookCategoryDto>(await bookCategoryService.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]BookCategoryDto entity)
        {
            return new ApiResponse("New book category added.",
                await bookCategoryService.Add(mapper.Map<BookCategory>(entity)));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]BookCategoryDto entity)
        {
            return new ApiResponse($"The record with {id} was updated.",
                await bookCategoryService.Update(id, mapper.Map<BookCategory>(entity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return new ApiResponse($"The record with {id} was deleted", await bookCategoryService.Delete(id));
        }
    }
}