using AspNetCoreApi.Api.Controllers.Base;
using AspNetCoreApi.Common.Constants;
using AspNetCoreApi.Models.Common.Identity;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Authorize(Policy = Role.User)]
    [ApiVersion(ApiConstants.Version1)]
    [ApiVersion(ApiConstants.Version2)]
    [Route(ApiConstants.BaseStatistics)]
    public partial class StatisticsController : BaseController
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper)
            : base(mapper)
        {
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
        }

        [MapToApiVersion(ApiConstants.Version1)]
        [ActionName("dashboard")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStatistics()
        {
            return new ApiResponse("Statistics retrived", await statisticsService.GetStatistics());
        }

        [MapToApiVersion(ApiConstants.Version2)]
        [ActionName("dashboard")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStatisticsV2()
        {
            return new ApiResponse("Statistics retrived", await statisticsService.GetStatistics());
        }
    }
}