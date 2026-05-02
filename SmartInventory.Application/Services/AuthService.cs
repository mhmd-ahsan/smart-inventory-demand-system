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

        public AuthService(
            IAuthRepository repository,
            IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        // REGISTER
        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<string>
                        .FailureResponse("Invalid request data");
                }

                var userExists =
                    await _repository.GetUserByEmailAsync(dto.Email);

                if (userExists != null)
                {
                    return ServiceResponse<string>
                        .FailureResponse("User already exists");
                }

                var user = new ApplicationUser
                {
                    FullName = dto.FullName,
                    Email = dto.Email,
                    UserName = dto.Email
                };

                var result =
                    await _repository.RegisterAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    return ServiceResponse<string>
                        .FailureResponse(
                            "Registration failed",
                            result.Errors.Select(e => e.Description).ToList()
                        );
                }

                var roleResult =
                    await _repository.AddToRoleAsync(user, Roles.User);

                if (!roleResult.Succeeded)
                {
                    return ServiceResponse<string>
                        .FailureResponse(
                            "Role assignment failed",
                            roleResult.Errors.Select(e => e.Description).ToList()
                        );
                }

                return ServiceResponse<string>
                    .SuccessResponse(
                        "User registered successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<string>
                    .FailureResponse(
                        $"Unexpected error during registration: {ex.Message}"
                    );
            }
        }

        // LOGIN
        public async Task<ServiceResponse<AuthResponseDto>> LoginAsync(LoginDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<AuthResponseDto>
                        .FailureResponse("Invalid login request");
                }

                var user =
                    await _repository.GetUserByEmailAsync(dto.Email);

                if (user == null)
                {
                    return ServiceResponse<AuthResponseDto>
                        .FailureResponse("Invalid email or password");
                }

                var validPassword =
                    await _repository.CheckPasswordAsync(user, dto.Password);

                if (!validPassword)
                {
                    return ServiceResponse<AuthResponseDto>
                        .FailureResponse("Invalid email or password");
                }

                var roles =
                    await _repository.GetRolesAsync(user);

                var token =
                    JwtHelper.GenerateToken(user, _configuration, roles);

                var response = new AuthResponseDto
                {
                    Token = token,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault()
                };

                return ServiceResponse<AuthResponseDto>
                    .SuccessResponse(
                        response,
                        "Login successful"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<AuthResponseDto>
                    .FailureResponse(
                        $"Unexpected error during login: {ex.Message}"
                    );
            }
        }
    }
}