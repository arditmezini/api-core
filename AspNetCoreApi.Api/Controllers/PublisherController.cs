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
    [Authorize(Policy = Role.Admin)]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/publisher")]
    public class PublisherController : BaseController
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
            : base(mapper)
        {
            this.publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse>> Get()
        {
            return new ApiResponse("Publishers retrived",
                mapper.Map<IEnumerable<PublisherDto>>(await publisherService.GetAll()));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse>> Get(int id)
        {
            return new ApiResponse($"Publisher with {id} retrived",
                mapper.Map<PublisherDto>(await publisherService.GetById(id)));
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Post([FromBody]PublisherDto entity)
        {
            return new ApiResponse("New publisher added.",
                await publisherService.Add(mapper.Map<Publisher>(entity)));
        }

        [HttpPut]
        public async Task<ActionResult<ApiResponse>> Put(int id, [FromBody]PublisherDto entity)
        {
            return new ApiResponse($"The record with {id} was updated.",
                await publisherService.Update(id, mapper.Map<Publisher>(entity)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> Delete(int id)
        {
            return new ApiResponse($"The record with {id} was deleted.", 
                await publisherService.Delete(id));
        }
    }
}