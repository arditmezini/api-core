using AspNetCoreApi.Models.Common.Identity;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Policy = Role.User)]
    [Route("api/data/[action]")]
    [ApiController]
    public class GeneralDataController : BaseController
    {
        private readonly IGeneralDataService generalDataService;
        private readonly RoleManager<IdentityRole> roleManager;

        public GeneralDataController(IGeneralDataService generalDataService, RoleManager<IdentityRole> roleManager,
            IMapper mapper)
            : base(mapper)
        {
            this.generalDataService = generalDataService ?? throw new ArgumentNullException(nameof(generalDataService));
            this.roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        [HttpGet]
        [ActionName("countries")]
        public async Task<ActionResult<ApiResponse>> GetCountries()
        {
            return new ApiResponse("Countries retrived",
                mapper.Map<IEnumerable<CountriesDto>>(await generalDataService.GetCountries()));
        }

        [HttpGet]
        [ActionName("roles")]
        public async Task<ActionResult<ApiResponse>> GetRoles()
        {
            return new ApiResponse("Roles retrived",
                mapper.Map<IEnumerable<RoleDto>>(roleManager.Roles.ToList()));
        }
    }
}