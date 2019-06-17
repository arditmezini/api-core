using AspNetCoreApi.Common.Logger;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration,
            IMapper mapper, ILogNLog logger, IOptions<AppConfig> appConfig)
            : base(mapper, logger, appConfig)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        public ActionResult<object> Login([FromBody] LoginDto login)
        {
            var result = signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.IsCompletedSuccessfully)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.Email);
                return GenerateJwtToken(login.Email, appUser);
            }
            throw new ApplicationException("Invalid login credentials");
        }

        [HttpPost]
        public ActionResult<object> Register([FromBody] RegisterDto model)
        {
            var appUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = userManager.CreateAsync(appUser, model.Password);
            if (result.IsCompletedSuccessfully)
            {
                signInManager.SignInAsync(appUser, false);
                return GenerateJwtToken(model.Email, appUser);
            }
            throw new ApplicationException("User registration failed");
        }

        private object GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtExpiredays"]));

            var token = new JwtSecurityToken(
                    configuration["JwtIssuer"],
                    configuration["JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}