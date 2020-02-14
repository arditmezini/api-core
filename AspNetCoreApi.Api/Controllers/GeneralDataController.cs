using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Roles = Role.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDataController : BaseController
    {
        private readonly IGeneralDataService generalDataService;

        public GeneralDataController(IGeneralDataService generalDataService, IMapper mapper)
            : base(mapper)
        {
            this.generalDataService = generalDataService ?? throw new ArgumentNullException(nameof(generalDataService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountriesDto>>> GetCountries()
        {
            return Ok(mapper.Map<IEnumerable<CountriesDto>>(await generalDataService.GetCountries()));
        }
    }
}