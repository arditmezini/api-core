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
    [ApiController]
    [Authorize(Policy = Role.User)]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/{version:apiVersion}/statistics/[action]")]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper)
            : base(mapper)
        {
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
        }

        [MapToApiVersion("1.0")]
        [ActionName("dashboard")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStatistics()
        {
            return new ApiResponse("Statistics retrived", await statisticsService.GetStatistics());
        }

        [MapToApiVersion("2.0")]
        [ActionName("dashboard")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStatisticsV2()
        {
            return new ApiResponse("Statistics retrived", await statisticsService.GetStatistics());
        }
    }
}