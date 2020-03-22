using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Extensions;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/author")]
    [ApiController]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService authorService;

        public AuthorController(IAuthorService authorService, IMapper mapper)
            : base(mapper)
        {
            this.authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            return new ApiResponse("Authors retrived",
                mapper.Map<IEnumerable<AuthorDto>>(await authorService.Get()), 200);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Author with {id} retrived",
                mapper.Map<AuthorDto>(await authorService.GetById(id)), 200);
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]AuthorDto entity)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());

            return new ApiResponse("New author added.",
                await authorService.Add(mapper.Map<Author>(entity)), 200);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]AuthorDto entity)
        {
            if (!ModelState.IsValid)
                throw new ApiException(ModelState.AllErrors());

            return new ApiResponse($"The record with {id} was updated.",
                await authorService.Update(id, mapper.Map<Author>(entity)), 200);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return new ApiResponse($"The record with {id} was deleted.", await authorService.Delete(id), 200);
        }
    }
}