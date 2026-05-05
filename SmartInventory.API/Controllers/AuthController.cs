using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.AuthDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Auth_Interface;
using SmartInventory.Domain.Constants;
using SmartInventory.Domain.Identity;
using System.Security.Claims;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(
            IAuthService authService,
            UserManager<ApplicationUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult>
            Register([FromBody] RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response =
                await _authService.RegisterAsync(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult>
            Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response =
                await _authService.LoginAsync(dto);

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
        [Authorize(Roles = Roles.Admin)]
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

        // USER + ADMIN
        [Authorize(Roles = $"{Roles.Admin},{Roles.User}")]
        [HttpGet("user-dashboard")]
        public IActionResult UserDashboard()
        {
            return Ok(new
            {
                success = true,
                message = "User Dashboard Access Granted"
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
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var email =
                User.FindFirst(ClaimTypes.Email)?.Value;

            var fullName =
                User.FindFirst(ClaimTypes.Name)?.Value;

            var roles = User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            return Ok(new
            {
                success = true,
                message = "Current User Info",
                data = new
                {
                    userId,
                    email,
                    fullName,
                    roles
                }
            });
        }

        // ASSIGN ADMIN ROLE
        // TEMPORARILY REMOVE AUTHORIZE
        // AFTER FIRST ADMIN CREATION ADD IT BACK

        //[Authorize(Roles = Roles.Admin)]
        [HttpPost("assign-admin/{userId}")]
        public async Task<IActionResult>
            AssignAdminRole(string userId)
        {
            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var isAlreadyAdmin =
                await _userManager.IsInRoleAsync(
                    user,
                    Roles.Admin);

            if (isAlreadyAdmin)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "User is already an Admin"
                });
            }

            await _userManager.AddToRoleAsync(
                user,
                Roles.Admin);

            return Ok(new
            {
                success = true,
                message = "Admin role assigned successfully"
            });
        }

        // REMOVE ADMIN ROLE
        [Authorize(Roles = Roles.Admin)]
        [HttpPost("remove-admin/{userId}")]
        public async Task<IActionResult>
            RemoveAdminRole(string userId)
        {
            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var isAdmin =
                await _userManager.IsInRoleAsync(
                    user,
                    Roles.Admin);

            if (!isAdmin)
            {
                return BadRequest(new
                {
                    success = false,
                    message = "User is not an Admin"
                });
            }

            await _userManager.RemoveFromRoleAsync(
                user,
                Roles.Admin);

            return Ok(new
            {
                success = true,
                message = "Admin role removed successfully"
            });
        }

        // GET USER ROLES
        [Authorize]
        [HttpGet("roles/{userId}")]
        public async Task<IActionResult>
            GetUserRoles(string userId)
        {
            var user =
                await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound(new
                {
                    success = false,
                    message = "User not found"
                });
            }

            var roles =
                await _userManager.GetRolesAsync(user);

            return Ok(new
            {
                success = true,
                data = roles
            });
        }
    }
}