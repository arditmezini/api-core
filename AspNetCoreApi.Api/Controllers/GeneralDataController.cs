using System.Collections.Generic;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralDataController : ControllerBase
    {
        private readonly IGeneralDataService generalDataService;

        public GeneralDataController(IGeneralDataService generalDataService)
        {
            this.generalDataService = generalDataService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CountriesDto>> GetCountries()
        {
            return Ok(generalDataService.GetCountries());
        }
    }
}