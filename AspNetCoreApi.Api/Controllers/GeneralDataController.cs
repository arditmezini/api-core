using System.Collections.Generic;
using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDataController : BaseController
    {
        private readonly IGeneralDataService generalDataService;

        public GeneralDataController(IGeneralDataService generalDataService, IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            : base(mapper, logger, appConfig)
        {
            this.generalDataService = generalDataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountriesDto>> GetCountries()
        {
            return Ok(_mapper.Map<IEnumerable<CountriesDto>>(generalDataService.GetCountries()));
        }
    }
}