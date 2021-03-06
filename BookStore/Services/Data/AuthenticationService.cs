﻿using BookStore.Constants;
using BookStore.Contracts.Repository;
using BookStore.Contracts.Services.Data;
using BookStore.Contracts.Services.General;
using BookStore.Models.Request;
using BookStore.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Services.Data
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IGenericRepository _genericRepository;
        private readonly ISettingsService _settingsService;
        private readonly IConnectionService _connectionService;

        public AuthenticationService(IGenericRepository genericRepository, ISettingsService settingsService, IConnectionService connectionService)
        {
            _genericRepository = genericRepository;
            _settingsService = settingsService;
            _connectionService = connectionService;
        }

        public async Task<UserResponse> Login(LoginRequest login)
        {
            var response = await _genericRepository.Post<UserResponse, LoginRequest>(ApiConstants.AccountLogin, login);
            if (response != null)
            {
                _settingsService.Username = response.Email;
                _settingsService.Token = response.Token;
            }
            return response;
        }

        public async Task<UserResponse> Register(RegisterRequest register)
        {
            var response = await _genericRepository.Post<UserResponse, RegisterRequest>(ApiConstants.AccountRegister, register);
            if (response != null)
            {
                _settingsService.Token = response.Token;
            }
            return response;
        }

        public async Task<bool> ValidateToken(string token)
        {
            var tokenRequest = new TokenRequest { Token = token };
            var response = await _genericRepository.Post<bool, TokenRequest>(ApiConstants.AccountValidateToken, tokenRequest);
            return response;
        }

        public async Task<List<RoleResponse>> GetRoles()
        {
            var response = await _genericRepository.Get<List<RoleResponse>>(ApiConstants.DataRoles);
            return response;
        }

        public async Task<UserResponse> Profile(string email)
        {
            var url = ApiConstants.AccountProfile.Replace("{email}", email);
            var response = await _genericRepository.Get<UserResponse>(url);
            return response;
        }

        public async Task<bool> IsUserAuthenticated()
        {
            if (!string.IsNullOrWhiteSpace(_settingsService.Token) && _connectionService.IsConnected)
                return await ValidateToken(_settingsService.Token);

            return false;
        }
    }
}