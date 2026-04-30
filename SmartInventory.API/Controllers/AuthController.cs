using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.AuthDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Auth_Interface;
using SmartInventory.Domain.Identity;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(dto);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = result.Message,
                    Data = null
                });
            }

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = result.Message,
                Data = result.Data
            });
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.LoginAsync(dto);

            if (result == null)
            {
                return Unauthorized(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid email or password",
                    Data = null
                });
            }

            return Ok(new ApiResponse<AuthResponseDto>
            {
                Success = true,
                Message = "Login successful",
                Data = result
            });
        }

        // PROTECTED ROUTE
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Authorized User",
                Data = "You accessed protected route"
            });
        }

        // ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Admin Access Granted",
                Data = "Welcome Admin"
            });
        }

        // HEALTH CHECK ENDPOINT
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "AuthController is reachable",
                Data = "pong"
            });
        }

        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var email = User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            return Ok(new
            {
                UserId = userId,
                Email = email
            });
        }

    }
}