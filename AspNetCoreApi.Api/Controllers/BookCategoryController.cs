using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/book-category")]
    [ApiController]
    public class BookCategoryController : BaseController
    {
        private readonly IBookCategoryService bookCategoryService;

        public BookCategoryController(IBookCategoryService bookCategoryService, IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            : base(mapper, logger, appConfig)
        {
            this.bookCategoryService = bookCategoryService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookCategoryDto>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<BookCategoryDto>>(bookCategoryService.GetAll()));
        }

        [HttpGet("{id}")]
        public APIResponse Get(int id)
        {
            return new APIResponse(200, $"Book category with {id} retrived.", bookCategoryService.GetById(id));
        }

        [HttpPost]
        public APIResponse Post([FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, "New book category added.", 
                bookCategoryService.Add(_mapper.Map<BookCategory>(entity)));
        }

        [HttpPut]
        public APIResponse Put(int id, [FromBody]BookCategoryDto entity)
        {
            return new APIResponse(200, $"The record with {id} was updated.", 
                bookCategoryService.Update(id, _mapper.Map<BookCategory>(entity)));
        }

        [HttpDelete("{id}")]
        public APIResponse Delete(int id)
        {
            return new APIResponse(200, $"The record with {id} was deleted", bookCategoryService.Delete(id));
        }
    }
}