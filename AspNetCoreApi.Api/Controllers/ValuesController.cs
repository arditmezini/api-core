using System.Collections.Generic;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAuthorService authorService;

        public ValuesController(IAuthorService authorService)
        {
            this.authorService = authorService;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<AuthorDto>> Get()
        {
            return Ok(authorService.Get());
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
