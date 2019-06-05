using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> Get()
        {
            return Ok(bookService.Get());
        }
    }
}