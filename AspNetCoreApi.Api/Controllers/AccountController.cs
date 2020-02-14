using AspNetCoreApi.Common.Mail;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Wrappers;
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
using System.Threading.Tasks;

namespace AspNetCoreApi.Api.Controllers
{
    [Route("api/account/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        protected readonly IMapper mapper;

        public AccountController(IEmailService emailService, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IConfiguration configuration, IMapper mapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [ActionName("login")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginDto login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.Email);
                await emailService.Send(MailHelper.BuildMail(MailTypeEnum.NewUser));
                return new ApiResponse("Login succesfully", await GenerateJwtToken(appUser), 200);
            }
            return new ApiResponse(401, new ApiError("Invalid login credentials"));
        }

        [ActionName("register")]
        [HttpPost]
        public async Task<ApiResponse> Register([FromBody] RegisterDto model)
        {
            var appUser = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(appUser, model.Role);
                await signInManager.SignInAsync(appUser, false);
                await emailService.Send(MailHelper.BuildMail(MailTypeEnum.LoginUser));
                return new ApiResponse("User registered.", await GenerateJwtToken(appUser), 200);
            }
            return new ApiResponse(400, new ApiError("User non registered"));
        }

        protected async Task<object> GenerateJwtToken(ApplicationUser user)
        {
            var userClaims = await GenerateUserClaims(user);

            return GenerateUserToken(userClaims);
        }

        private async Task<List<Claim>> GenerateUserClaims(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier,user.Id)
            };

            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            return claims;
        }

        private object GenerateUserToken(List<Claim> userClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtConfig:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(configuration["JwtConfig:JwtExpiredays"]));

            var token = new JwtSecurityToken(
                    issuer: configuration["JwtConfig:JwtIssuer"],
                    audience: configuration["JwtConfig:JwtIssuer"],
                    claims: userClaims,
                    notBefore: DateTime.UtcNow,
                    expires: expires,
                    signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}