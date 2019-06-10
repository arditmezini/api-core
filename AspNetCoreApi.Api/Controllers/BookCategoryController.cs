using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/book-category")]
    [ApiController]
    public class BookCategoryController : ControllerBase
    {
        private readonly IBookCategoryService bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService)
        {
            this.bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookCategoryDto>> Get()
        {
            return Ok(bookCategoryService.Get());
        }

        [HttpGet("{id}")]
        public APIResponse Get(int id)
        {
            return new APIResponse(200, $"Book category with {id} retrived.", bookCategoryService.GetById(id));
        }

        [HttpPost]
        public APIResponse Post([FromBody]BookCategoryDto entity)
        {
            bookCategoryService.Add(entity);
            return new APIResponse(200, "New book category added.");
        }

        [HttpPut]
        public APIResponse Put(int id, [FromBody]BookCategoryDto entity)
        {
            bookCategoryService.Update(id, entity);
            return new APIResponse(200, $"The record with {id} was updated.");
        }

        [HttpDelete("{id}")]
        public APIResponse Delete(int id)
        {
            bookCategoryService.Delete(id);
            return new APIResponse(200, $"The record with {id} was deleted");
        }
    }
}