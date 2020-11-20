using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using System;

namespace AspNetCoreApi.Api.Hubs
{
    public class BaseHub : Hub
    {
        protected readonly IMapper mapper;

        protected BaseHub(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
    }
}