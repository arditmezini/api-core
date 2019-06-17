using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
            this.bookCategoryService = bookCategoryService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookCategoryDto>> Get()
        {
            return Ok(mapper.Map<IEnumerable<BookCategoryDto>>(bookCategoryService.GetAll()));
        }

        [HttpGet("{id}")]
        public ActionResult<APIResponse> Get(int id)
        {
            return new APIResponse(200, $"Book category with {id} retrived.", bookCategoryService.GetById(id));
        }

        [HttpPost]
        public ActionResult<APIResponse> Post([FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, "New book category added.",
                bookCategoryService.Add(mapper.Map<BookCategory>(entity)));
        }

        [HttpPut]
        public ActionResult<APIResponse> Put(int id, [FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, $"The record with {id} was updated.",
                bookCategoryService.Update(id, mapper.Map<BookCategory>(entity)));
        }

        [HttpDelete("{id}")]
        public ActionResult<APIResponse> Delete(int id)
        {
            return new APIResponse(200, $"The record with {id} was deleted", bookCategoryService.Delete(id));
        }
    }
}