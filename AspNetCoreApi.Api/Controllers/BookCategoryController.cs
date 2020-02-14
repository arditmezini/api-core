using AspNetCoreApi.Dal.Entities;
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
    [Authorize(Roles = Role.Admin)]
    [Route("api/book-category")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService bookCategoryService;
        protected readonly IMapper mapper;

        public BookCategoryController(IBookCategoryService bookCategoryService, IMapper mapper)
        {
            this.bookCategoryService = bookCategoryService ?? throw new ArgumentNullException(nameof(bookCategoryService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookCategoryDto>>> Get()
        {
            return Ok(mapper.Map<IEnumerable<BookCategoryDto>>(await bookCategoryService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Book category with {id} retrived.", await bookCategoryService.GetById(id), 200);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]BookCategoryDto entity)
        {
            return new ApiResponse("New book category added.",
                await bookCategoryService.Add(mapper.Map<BookCategory>(entity)), 200);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]BookCategoryDto entity)
        {
            return new ApiResponse($"The record with {id} was updated.",
                await bookCategoryService.Update(id, mapper.Map<BookCategory>(entity)), 200);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return new ApiResponse($"The record with {id} was deleted", await bookCategoryService.Delete(id), 200);
        }
    }
}