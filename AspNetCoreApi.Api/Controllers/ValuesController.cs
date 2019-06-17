using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        
        private IAuthorService _authorService;
        protected readonly IMapper _mapper;
        protected readonly ILogNLog _logger;
        protected AppConfig _appConfig { get; set; }

        public ValuesController(IAuthorService authorService, IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            //:base(mapper, logger, appConfig)
           
        {
            _mapper = mapper;
            _logger = logger;
            _appConfig = appConfig.Value;
            _authorService = authorService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(_authorService.Get()));
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
