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
    [Route("api/statistics/[action]")]
    [ApiController]
    public class StatisticsController : BaseController
    {
        private readonly IStatisticsService statisticsService;

        public StatisticsController(IStatisticsService statisticsService, IMapper mapper)
            : base(mapper)
        {
            this.statisticsService = statisticsService ?? throw new ArgumentNullException(nameof(statisticsService));
        }

        [ActionName("dashboard")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> GetStatistics()
        {
            return new ApiResponse("Statistics retrived", await statisticsService.GetStatistics());
        }
    }
}