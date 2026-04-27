using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.DTOs.SupplierDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface;
using SmartInventory.Domain.Entities;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(ISupplierService service)
        {
            _service = service;
        }

        // Get All Supplier {Get}
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllSupplier();
            return Ok(new ApiResponse<IEnumerable<SupplierReadDto>>
            {
                Message = "Suppliers fetched",
                Success = true,
                Data = result
            });
        }

        // Get Supplier By Id 
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetSupplierById(id);

            if(result == null)
            {
                return NotFound(new ApiResponse<string>
                {
                    Message = "Supplier not found",
                    Success = true,
                    Data = null
                });
            }

            return Ok(new ApiResponse<SupplierReadDto>
            {
                Message = "Supplier found",
                Success = true,
                Data = result
            });
        }

        // Add Supplier
        [HttpPost("add-supplier")]
        public async Task<IActionResult> AddAsync(CreateSupplierDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.AddSupplier(dto);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Supplier created successfully",
                Data = "Created"
            });
        }

        // Update Supplier
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSupplier(int id, [FromBody] UpdateSupplierDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.UpdateSupplier(id, dto);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Supplier updated successfully",
                Data = "Updated"
            });
        }

        // Delete Supplier
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSupplier(int id)
        {
            await _service.DeleteSupplier(id);

            return Ok(
                new ApiResponse<string>
                {
                    Success = true,
                    Message = "Supplier deleted successfully",
                    Data = "Deleted"
                });
        }
    }
}