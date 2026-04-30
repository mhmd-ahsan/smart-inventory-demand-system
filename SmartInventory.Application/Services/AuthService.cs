using Microsoft.Extensions.Configuration;
using SmartInventory.Application.Common.Helpers;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.AuthDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Auth_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Auth_Interface;
using SmartInventory.Domain.Constants;
using SmartInventory.Domain.Identity;

namespace SmartInventory.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _repository;

        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        // Register
        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto)
        {
            var userExists =
                await _repository
                .GetUserByEmailAsync(
                    dto.Email);

            if (userExists != null)
                return ServiceResponse<string>.Fail("User already exists");

            var user =
                new ApplicationUser
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    UserName = dto.Email
                };

            var result = await _repository.RegisterAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                return ServiceResponse<string>.Fail(
                    string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            var roleResult =
                await _repository.AddToRoleAsync(user, Roles.User);

            if (!roleResult.Succeeded)
            {
                return ServiceResponse<string>.Fail(
                    string.Join(", ", roleResult.Errors.Select(e => e.Description)));
            }

            return ServiceResponse<string>.Ok("User registered successfully");
        }

        // Login
        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var validPassword = await _repository.CheckPasswordAsync(user, dto.Password);

            if (!validPassword)
                return null;

            var roles = await _repository.GetRolesAsync(user);

            var token =
                JwtHelper.GenerateToken(user, _configuration, roles);

            return new AuthResponseDto
            {
                Token = token,
                Email = user.Email,
                FullName = user.FullName,
                Role = roles.FirstOrDefault()
            };
        }
    }
}