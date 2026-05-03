using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Inventory_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryLogsController
        : ControllerBase
    {
        private readonly IInventoryLogService _service;

        public InventoryLogsController(
            IInventoryLogService service)
        {
            _service = service;
        }

        // GET: api/inventorylogs/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response =
                await _service.GetAllAsync();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // GET: api/inventorylogs/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult>
            GetById(int id)
        {
            var response =
                await _service.GetByIdAsync(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/inventorylogs/add-log
        [HttpPost("add-log")]
        public async Task<IActionResult>
            AddLog(CreateInventoryLogDto dto)
        {
            var response =
                await _service.CreateAsync(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // PUT: api/inventorylogs/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult>
            UpdateLog(
                int id,
                UpdateInventoryLogDto dto)
        {
            var response =
                await _service.UpdateAsync(id, dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // DELETE: api/inventorylogs/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            DeleteLog(int id)
        {
            var response =
                await _service.DeleteAsync(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}