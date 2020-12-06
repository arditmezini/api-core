using AspNetCoreApi.Common.LinqExtensions;
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
    [ApiController]
    [Authorize(Policy = Role.User)]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/data/[action]")]
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

        [AllowAnonymous]
        [HttpGet]
        [ActionName("roles")]
        public async Task<ActionResult<ApiResponse>> GetRoles()
        {
            List<IdentityRole> roles = new List<IdentityRole>();
            await Task.Run(() =>
            {
                roles = roleManager.Roles.WhereIf(UserRole == Role.User || UserRole == null, x => x.Name == Role.User).ToList();
            });

            return new ApiResponse("Roles retrived", mapper.Map<IEnumerable<RoleDto>>(roles));
        }
    }
}