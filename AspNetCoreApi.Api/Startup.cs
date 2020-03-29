using AspNetCoreApi.Api.Configurations;
using AspNetCoreApi.Dal.Extensions;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Common.Emails;
using AspNetCoreApi.Service;
using AutoMapper;
using AutoWrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            services.AddDbContextWithLazyLoading(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.ConfigureHangfire(Configuration.GetConnectionString("HangfireConnection"));

            services.Configure<AppConfig>(Configuration.GetSection(nameof(AppConfig)));

            var corsOptions = Configuration.GetGeneric<CorsOptions>("CorsOptions");
            services.ConfigureCors(corsOptions.PolicyName, corsOptions.CorsOrigin);

            services.AddControllers();

            services.ConfigureSwagger();

            services.RegisterServicesDependencyInjection();

            services.ConfigureIdentity();
            var jwtConfig = Configuration.GetGeneric<JwtConfig>("JwtConfig");
            services.ConfigureJwt(jwtConfig.JwtIssuer, jwtConfig.JwtKey);

            services.ConfigureMvc();

            var mailConfig = Configuration.GetGeneric<EmailConfiguration>("EmailConfiguration");
            services.ConfigureMailKit(mailConfig);

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

            //Generic API Response
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                IsApiOnly = false,
                EnableResponseLogging = true,
                EnableExceptionLogging = true
            });

            var corsOptions = Configuration.GetGeneric<CorsOptions>("CorsOptions");
            app.UseCorsPolicy(corsOptions.PolicyName);

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

            //app.UseMvc();
        }
    }
}