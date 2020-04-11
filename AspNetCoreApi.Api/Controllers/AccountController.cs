using AspNetCoreApi.Common.Mail;
using AspNetCoreApi.Models.Common;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Extensions;
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
    public class AccountController : BaseController
    {
        private readonly IEmailService emailService;
        private readonly IHangfireJobService hangfireJobService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(IEmailService emailService, IHangfireJobService hangfireJobService,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IConfiguration configuration, IMapper mapper)
            : base(mapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.hangfireJobService = hangfireJobService ?? throw new ArgumentNullException(nameof(hangfireJobService));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [ActionName("login")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginDto login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.Email);
                var mailBuilder = MailHelper.BuildMail(MailTypeEnum.NewUser);
                hangfireJobService.ProcessFireAndForgetJobs<IEmailService>(x => x.Send(mailBuilder));
                return new ApiResponse("Login succesfully", await GenerateJwtToken(appUser));
            }
            return new ApiResponse(401, new ApiError("Invalid login credentials"));
        }

        [ActionName("register")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Register([FromBody] RegisterDto model)
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
                var mailBuilder = MailHelper.BuildMail(MailTypeEnum.LoginUser);
                hangfireJobService.ProcessFireAndForgetJobs<IEmailService>(x => x.Send(mailBuilder));
                return new ApiResponse("User registered.", await GenerateJwtToken(appUser));
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
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["JwtConfig:JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(configuration["JwtConfig:JwtExpireDays"]));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Issuer = configuration["JwtConfig:JwtIssuer"],
                Audience = configuration["JwtConfig:JwtIssuer"],
                Expires = expires,
                SigningCredentials = creds
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}