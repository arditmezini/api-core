using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : BaseController
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService, IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            : base(mapper, logger, appConfig)
        {
            this.bookService = bookService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDto>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<BookDto>>(bookService.GetAll()));
        }
    }
}