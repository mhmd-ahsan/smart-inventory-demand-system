using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.AuthDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Auth_Interface;

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

            var response = await _authService.RegisterAsync(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _authService.LoginAsync(dto);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }

        // PROTECTED ROUTE
        [Authorize]
        [HttpGet("protected")]
        public IActionResult Protected()
        {
            return Ok(new
            {
                success = true,
                message = "Authorized User",
                data = "You accessed protected route"
            });
        }

        // ADMIN ONLY
        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnly()
        {
            return Ok(new
            {
                success = true,
                message = "Admin Access Granted",
                data = "Welcome Admin"
            });
        }

        // HEALTH CHECK
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok(new
            {
                success = true,
                message = "AuthController is reachable",
                data = "pong"
            });
        }

        // WHO AM I
        [Authorize]
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var userId =
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            var email =
                User.FindFirst(System.Security.Claims.ClaimTypes.Email)?.Value;

            return Ok(new
            {
                success = true,
                data = new
                {
                    userId,
                    email
                }
            });
        }
    }
}