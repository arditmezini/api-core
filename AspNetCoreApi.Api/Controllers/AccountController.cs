﻿using AspNetCoreApi.Api.Controllers.Base;
using AspNetCoreApi.Common.Constants;
using AspNetCoreApi.Common.Mail;
using AspNetCoreApi.Models.Common.Configurations;
using AspNetCoreApi.Models.Common.Emails;
using AspNetCoreApi.Models.Dto;
using AspNetCoreApi.Models.Entity;
using AspNetCoreApi.Service.Contracts;
using AutoMapper;
using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AspNetCoreApi.Api.Controllers
{
    [ApiVersion(ApiConstants.Version1)]
    [Route(ApiConstants.BaseAccount)]
    public class AccountController : BaseController
    {
        private readonly IEmailService emailService;
        private readonly IHangfireJobService hangfireJobService;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly JwtConfig jwtOptions;

        public AccountController(IEmailService emailService, IHangfireJobService hangfireJobService,
            SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
            IOptions<JwtConfig> jwtOptions, IMapper mapper)
            : base(mapper)
        {
            this.emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            this.hangfireJobService = hangfireJobService ?? throw new ArgumentNullException(nameof(hangfireJobService));
            this.signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.jwtOptions = jwtOptions.Value ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        [ActionName("validate")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> ValidateToken(TokenDto token)
        {
            try
            {
                await Task.Run(() =>
                {
                    var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.JwtKey));
                    var validationParams = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = jwtOptions.JwtIssuer,
                        ValidAudience = jwtOptions.JwtAudience,
                        IssuerSigningKey = key
                    };
                    var principal = new JwtSecurityTokenHandler()
                        .ValidateToken(token.Token, validationParams, out SecurityToken validatedToken);
                });

                return new ApiResponse("Token validated succesfully", true);
            }
            catch (Exception)
            {
                return new ApiResponse("Token failed validation", false);
            }
        }

        [ActionName("login")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> Login([FromBody] LoginDto login)
        {
            var result = await signInManager.PasswordSignInAsync(login.Email, login.Password, false, false);
            if (result.Succeeded)
            {
                var appUser = userManager.Users.SingleOrDefault(x => x.Email == login.Email);
                var mailBuilder = MailHelper.BuildMail(
                    MailTypeEnum.LoginUser,
                    new EmailAddress { Name = "Support", Address = "support@site.com" },
                    new EmailAddress { Name = appUser.FirstName, Address = appUser.Email });
                hangfireJobService.ProcessFireAndForgetJobs<IEmailService>(x => x.Send(mailBuilder));
                var user = mapper.Map<UserDto>(appUser);
                user.Token = (string)await GenerateJwtToken(appUser);
                return new ApiResponse("Login succesfully", user);
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
                var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(appUser);
                var confirmationLink = BaseUrl + $"/api/{ApiVersion}/account/confirm-email?token={HttpUtility.UrlEncode(confirmationToken)}&email={appUser.Email}";

                await userManager.AddToRoleAsync(appUser, model.Role);
                await signInManager.SignInAsync(appUser, false);
                var mailBuilder = MailHelper.BuildMail(MailTypeEnum.NewUser,
                    new EmailAddress { Name = "Support", Address = "support@site.com" },
                    new EmailAddress { Name = appUser.FirstName, Address = appUser.Email },
                    confirmationLink);
                hangfireJobService.ProcessFireAndForgetJobs<IEmailService>(x => x.Send(mailBuilder));
                var user = mapper.Map<UserDto>(appUser);
                user.Token = (string)await GenerateJwtToken(appUser);
                return new ApiResponse("User registered.", user);
            }
            return new ApiResponse(400, new ApiError("User non registered"));
        }

        [Authorize]
        [ActionName("profile")]
        [HttpGet("{email}")]
        public async Task<ActionResult<ApiResponse>> Profile([Required, EmailAddress] string email)
        {
            var appUser = userManager.Users.SingleOrDefault(x => x.Email == email);
            if (appUser == null)
                return new ApiResponse(404, new ApiError("User not found"));

            var roles = await userManager.GetRolesAsync(appUser);

            var user = mapper.Map<UserDto>(appUser);
            user.Role = string.Join(',', roles);
            return new ApiResponse("Profile data", user);
        }

        [ActionName("confirm-email")]
        [HttpGet]
        public async Task<ActionResult<ApiResponse>> ConfirmEmail([Required] string token, [Required, EmailAddress] string email)
        {
            var appUser = await userManager.FindByEmailAsync(email);
            if (appUser == null)
                return new ApiResponse(404, new ApiError("User not found"));

            var result = await userManager.ConfirmEmailAsync(appUser, token);
            if (!result.Succeeded)
                return new ApiResponse("Email not confirmend", false);

            return new ApiResponse("Email confirmend", true);
        }

        [ActionName("forget-password")]
        [HttpPost("{email}")]
        public async Task<ActionResult<ApiResponse>> ForgetPassword([Required, EmailAddress] string email)
        {
            var appUser = await userManager.FindByEmailAsync(email);
            if (appUser == null)
                return new ApiResponse(404, new ApiError("User not found"));

            var passwordToken = await userManager.GeneratePasswordResetTokenAsync(appUser);
            var passwordLink = $"urltofrontend?token={HttpUtility.UrlEncode(passwordToken)}&email={email}";

            var mailBuilder = MailHelper.BuildMail(MailTypeEnum.NewUser,
                    new EmailAddress { Name = "Support", Address = "support@site.com" },
                    new EmailAddress { Name = appUser.FirstName, Address = appUser.Email },
                    passwordLink);
            hangfireJobService.ProcessFireAndForgetJobs<IEmailService>(x => x.Send(mailBuilder));

            return new ApiResponse("Reset password link was sent", true);
        }

        [ActionName("reset-password")]
        [HttpPost]
        public async Task<ActionResult<ApiResponse>> ResetPassword(ResetPasswordDto model)
        {
            var appUser = await userManager.FindByEmailAsync(model.Email);
            if (appUser == null)
                return new ApiResponse(404, new ApiError("User not found"));

            var resetPasswordResult = await userManager.ResetPasswordAsync(appUser, model.Token, model.Password);
            if (!resetPasswordResult.Succeeded)
                return new ApiResponse("Password reset error", false);

            return new ApiResponse("Password reset correctly", true);

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
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddDays(Convert.ToDouble(jwtOptions.JwtExpireDays));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(userClaims),
                Issuer = jwtOptions.JwtIssuer,
                Audience = jwtOptions.JwtAudience,
                Expires = expires,
                SigningCredentials = creds,
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}