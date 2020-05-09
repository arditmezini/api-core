using AspNetCoreApi.Api.Configurations;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Models.Common.Configurations;
using AspNetCoreApi.Service;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AspNetCoreApi.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureHealthChecks(Configuration);

            services.AddDbContextWithLazyLoading(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null
                        );
                    }
                 )
            );

            services.ConfigureHangfire(Configuration);

            services.Configure<AppConfig>(Configuration.GetSection(nameof(AppConfig)));
            services.Configure<JwtConfig>(Configuration.GetSection(nameof(JwtConfig)));

            services.ConfigureResponseCompression();

            services.ConfigureCors(Configuration);

            services.AddControllers();

            services.ConfigureSwagger();

            services.RegisterServicesDependencyInjection();

            services.ConfigureIdentity();

            services.ConfigureJwt(Configuration);

            services.ConfigureMvc();

            services.ConfigureMailKit(Configuration);

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostEnvironment env)
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

            app.UseResponseCompression();

            app.UseAutoWrapper();

            app.UseCorsPolicy(Configuration);

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(ep =>
            {
                ep.MapControllers();
            });

            app.UseHangfire();

            if (env.IsDevelopment())
            {
                app.UseSwaggerWithUI();
            }

            app.UseHealthChecks(Configuration);
        }
    }
}