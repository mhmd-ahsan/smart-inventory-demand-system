using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.SupplierDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _service;

        public SupplierController(
            ISupplierService service)
        {
            _service = service;
        }

        // GET: api/supplier/get-all
        [Authorize(Roles = "Admin,User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response =
                await _service.GetAllSupplier();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // GET: api/supplier/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]

        public async Task<IActionResult>
            GetById(int id)
        {
            var response =
                await _service.GetSupplierById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/supplier/add-supplier
        [Authorize(Roles = "Admin")]
        [HttpPost("add-supplier")]

        public async Task<IActionResult>
            AddSupplier(CreateSupplierDto dto)
        {
            var response =
                await _service.AddSupplier(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // PUT: api/supplier/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult>
            UpdateSupplier(
                int id,
                UpdateSupplierDto dto)
        {
            var response =
                await _service.UpdateSupplier(id, dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // DELETE: api/supplier/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult>
            DeleteSupplier(int id)
        {
            var response =
                await _service.DeleteSupplier(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}