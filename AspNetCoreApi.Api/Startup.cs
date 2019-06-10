using AspNetCoreApi.Api.Configurations;
using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Data.DataContext;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using VMD.RESTApiResponseWrapper.Core.Extensions;

namespace AspNetCoreApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            var logDirectory = string.Concat(Directory.GetCurrentDirectory());
            LoggerExtension.ConfigureNLogStartup(logDirectory);

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextWithLazyLoading<ApiContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<AppConfig>(Configuration.GetSection(nameof(AppConfig)));

            services.ConfigureCors(
               Configuration.GetGenericValue<string>("CorsOptions:PolicyName"),
               Configuration.GetGenericValue<string>("CorsOptions:CorsOrigin"));

            services.ConfigureSwagger();

            services.RegisterNLog();

            services.RegisterServicesDependencyInjection();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.ConfigureCorsGlobally(Configuration.GetGenericValue<string>("CorsOptions:PolicyName"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            //Generic API Response
            app.UseAPIResponseWrapperMiddleware();

            app.UseCorsPolicy(Configuration.GetGenericValue<string>("CorsOptions:PolicyName"));

            if (env.IsDevelopment())
            {
                app.UseSwaggerWithUI();
            }

            app.UseMvc();
        }
    }
}
