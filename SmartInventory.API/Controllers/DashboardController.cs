using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Interfaces.Service_Interfaces.Dashboard_Interface;
using SmartInventory.Application.Services;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _service;

        public DashboardController(IDashboardService service)
        {
            _service = service;
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("analytics")]
        public async Task<IActionResult> GetDashboardAnalytics()
        {
            var response = await _service.GetDashboardDataAsync();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);

        }
    }
}
