using AspNetCoreApi.Common.Constants;
using AspNetCoreApi.Models.Common.Identity;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Hubs
{
    [Authorize(Policy = Role.User)]
    public class NewsHub : BaseHub
    {
        private readonly INewsService newsService;

        public NewsHub(INewsService newsService, IMapper mapper)
            : base(mapper)
        {
            this.newsService = newsService ?? throw new ArgumentNullException(nameof(newsService));
        }

        public async Task OpenNews()
        {
            IEnumerable<News> news = await newsService.GetAll();
            await Clients.All.SendAsync(HubConstants.OpenNews, mapper.Map<IEnumerable<NewsDto>>(news));
        }

        public async Task SendNews(NewsDto entity)
        {
            bool addNews = await newsService.Add(mapper.Map<News>(entity));
            if (addNews)
            {
                await Clients.All.SendAsync(HubConstants.SendNews, mapper.Map<NewsDto>(entity));
            }
        }

        public async Task CloseNews()
        {
            await Clients.All.SendAsync(HubConstants.CloseNews, new List<NewsDto>());
        }
    }
}