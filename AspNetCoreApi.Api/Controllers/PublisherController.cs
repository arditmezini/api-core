using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;
        protected readonly IMapper mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            this.publisherService = publisherService ?? throw new ArgumentNullException(nameof(publisherService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherDto>>> Get()
        {
            return Ok(mapper.Map<IEnumerable<PublisherDto>>(await publisherService.GetAll()));
        }
    }
}