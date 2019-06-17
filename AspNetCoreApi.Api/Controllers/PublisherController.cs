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
    public class PublisherController : BaseController
    {
        private readonly IPublisherService publisherService;

        public PublisherController(IPublisherService publisherService, IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            : base(mapper, logger, appConfig)
        {
            this.publisherService = publisherService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PublisherDto>> Get()
        {
            return Ok(_mapper.Map<IEnumerable<PublisherDto>>(publisherService.GetAll()));
        }
    }
}