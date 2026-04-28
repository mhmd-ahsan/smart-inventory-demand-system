using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using SmartInventory.Application.Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Inventory_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryLogsController : ControllerBase
    {
        private readonly IInventoryLogService _service;

        public InventoryLogsController(
            IInventoryLogService service)
        {
            _service = service;
        }

        // Get All Logs
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result =
                await _service.GetAllAsync();

            return Ok(
                new ApiResponse<
                    IEnumerable<InventoryLogReadDto>>
                {
                    Success = true,
                    Message = "Inventory logs fetched",
                    Data = result
                });
        }

        // Get Log By Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult>
        GetById(int id)
        {
            var log =
               await _service.GetByIdAsync(id);

            if (log is null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Inventory log not found",
                        Data = null
                    });
            }

            return Ok(
                new ApiResponse<
                    InventoryLogReadDto>
                {
                    Success = true,
                    Message = "Inventory log found",
                    Data = log
                });
        }

        // Add Inventory Log
        [HttpPost("add-log")]
        public async Task<IActionResult>
        AddLog(
         [FromBody]
         CreateInventoryLogDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateAsync(dto);

            return Ok(
                new ApiResponse<string>
                {
                    Success = true,
                    Message =
                       "Inventory log created successfully",
                    Data = "Created"
                });
        }

        // Update Inventory Log
        [HttpPut("{id:int}")]
        public async Task<IActionResult>
        UpdateLog(
            int id,
            [FromBody]
            UpdateInventoryLogDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.UpdateAsync(
                id,
                dto);

            return Ok(
               new ApiResponse<string>
               {
                   Success = true,
                   Message =
                     "Inventory log updated successfully",
                   Data = "Updated"
               });
        }

        // Delete Inventory Log
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
        DeleteLog(int id)
        {
            await _service.DeleteAsync(id);

            return Ok(
              new ApiResponse<string>
              {
                  Success = true,
                  Message =
                    "Inventory log deleted successfully",
                  Data = "Deleted"
              });
        }
    }
}