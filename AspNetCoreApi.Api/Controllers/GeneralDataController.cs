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
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDataController : ControllerBase
    {
        private readonly IGeneralDataService generalDataService;
        protected readonly IMapper mapper;

        public GeneralDataController(IGeneralDataService generalDataService, IMapper mapper)
        {
            this.generalDataService = generalDataService;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountriesDto>> GetCountries()
        {
            return Ok(mapper.Map<IEnumerable<CountriesDto>>(generalDataService.GetCountries()));
        }
    }
}