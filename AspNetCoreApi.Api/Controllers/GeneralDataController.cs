using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDataController : ControllerBase
    {
        private readonly IGeneralDataService generalDataService;
        protected readonly IMapper mapper;

        public GeneralDataController(IGeneralDataService generalDataService, IMapper mapper)
        {
            this.generalDataService = generalDataService ?? throw new ArgumentNullException(nameof(generalDataService));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountriesDto>>> GetCountries()
        {
            return Ok(mapper.Map<IEnumerable<CountriesDto>>(await generalDataService.GetCountries()));
        }
    }
}