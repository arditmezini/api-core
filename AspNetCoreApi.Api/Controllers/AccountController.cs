using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using VMD.RESTApiResponseWrapper.Core.Wrappers;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        protected readonly IMapper mapper;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IMapper mapper)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [ActionName("login")]
        [HttpPost]
        public ActionResult<APIResponse> Login([FromBody] LoginDto login)
        {
            var result = signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.Email);
                return new APIResponse(200, "Login succesfully", GenerateJwtToken(login.Email, appUser));
            }
            return new APIResponse(401, "Invalid login credentials", null, new ApiError("Invalid login credentials"));
        }

        [ActionName("register")]
        [HttpPost]
        public ActionResult<APIResponse> Register([FromBody] RegisterDto model)
        {
            var appUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = userManager.CreateAsync(appUser, model.Password);
            if (result.Result.Succeeded)
            {
                userManager.AddToRoleAsync(appUser, model.Role);
                signInManager.SignInAsync(appUser, false);
                return new APIResponse(200, "User registered.", GenerateJwtToken(model.Email, appUser));
            }
            return new APIResponse(400, "User non registered", null, new ApiError("User non registered"));
        }

        protected object GenerateJwtToken(string email, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtConfig:JwtExpiredays"]));

            var token = new JwtSecurityToken(
                    configuration["JwtConfig:JwtIssuer"],
                    configuration["JwtConfig:JwtIssuer"],
                    claims,
                    expires: expires,
                    signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}