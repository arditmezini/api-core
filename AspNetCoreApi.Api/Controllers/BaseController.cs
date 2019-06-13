using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Models.Common;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreApi.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly ILogNLog _logger;
        protected AppConfig _appConfig { get; set; }

        public BaseController(IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
        {
            _mapper = mapper;
            _logger = logger;
            _appConfig = appConfig.Value;
        }
    }
}