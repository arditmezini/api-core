using AspNetCoreApi.Models.Common;
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
        public async Task<ActionResult<ApiResponse>> GetCountries()
        {
            return new ApiResponse("Countries retrived",
                mapper.Map<IEnumerable<CountriesDto>>(await generalDataService.GetCountries()), 200);
        }
    }
}