using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService publisherService;
        protected readonly IMapper mapper;

        public PublisherController(IPublisherService publisherService, IMapper mapper)
        {
            this.publisherService = publisherService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PublisherDto>> Get()
        {
            return Ok(mapper.Map<IEnumerable<PublisherDto>>(publisherService.GetAll()));
        }
    }
}