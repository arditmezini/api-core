using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<APIResponse>> Get(int id)
        {
            return new APIResponse(200, $"Book category with {id} retrived.", await bookCategoryService.GetById(id));
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse>> Post([FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, "New book category added.",
                await bookCategoryService.Add(mapper.Map<BookCategory>(entity)));
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse>> Put(int id, [FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, $"The record with {id} was updated.",
                await bookCategoryService.Update(id, mapper.Map<BookCategory>(entity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<APIResponse>> Delete(int id)
        {
            return new APIResponse(200, $"The record with {id} was deleted", await bookCategoryService.Delete(id));
        }
    }
}