using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.SaleDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Sales_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _service;
        
        public SalesController(ISaleService service)
        {
            _service = service; 
        }

        // Get All
        [Authorize(Roles = "Admin,User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response  =  await _service.GetAllSales();

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // Get by Id
        [Authorize(Roles = "Admin")]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetSaleById(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // Add Sale
        [Authorize(Roles = "Admin,User")]
        [HttpPost("add-sale")]
        public async Task<IActionResult> AddSale(CreateSaleDto dto)
        {
            var response = await _service.CreateSale(dto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // Update {Put}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSale(int id,UpdateSaleDto dto)
        {

            var response = await _service.UpdateSale(id, dto);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        // Delete 
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var response = await _service.DeleteSale(id);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
