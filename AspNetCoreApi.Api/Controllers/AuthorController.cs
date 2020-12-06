using AspNetCoreApi.Common.Constants;
using AspNetCoreApi.Common.LinqExtensions;
using AspNetCoreApi.Dal.Entities;
using AspNetCoreApi.Models.Common.Identity;
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
    [ApiController]
    [Authorize(Policy = Role.Manager)]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/author")]
    public class AuthorController : BaseController
    {
        private readonly IAuthorService authorService;
        private readonly ICacheService cacheService;

        public AuthorController(IAuthorService authorService, ICacheService cacheService, IMapper mapper)
            : base(mapper)
        {
            this.authorService = authorService ?? throw new ArgumentNullException(nameof(authorService));
            this.cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            var authors = await cacheService.GetAsync<IEnumerable<AuthorDto>>(CacheConstants.AuthorList);
            if (!authors.AnyOrDefault())
            {
                authors = mapper.Map<IEnumerable<AuthorDto>>(await authorService.Get());
                await cacheService.SetAsync(CacheConstants.AuthorList, authors);
            }
            return new ApiResponse("Authors retrived", authors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Author with {id} retrived",
                mapper.Map<AuthorDto>(await authorService.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]AuthorDto entity)
        {
            var createdAuthor = await authorService.Add(mapper.Map<Author>(entity));

            if (createdAuthor)
                await cacheService.RemoveAsync(CacheConstants.AuthorList);

            return new ApiResponse("New author added.", createdAuthor);
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]AuthorDto entity)
        {
            var updatedAuthor = await authorService.Update(id, mapper.Map<Author>(entity));

            if (updatedAuthor)
                await cacheService.RemoveAsync(CacheConstants.AuthorList);

            return new ApiResponse($"The record with {id} was updated.", updatedAuthor);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            var deletedAuthor = await authorService.Delete(id);

            if (deletedAuthor)
                await cacheService.RemoveAsync(CacheConstants.AuthorList);

            return new ApiResponse($"The record with {id} was deleted.", deletedAuthor);
        }
    }
}