using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(
            IProductService service)
        {
            _service = service;
        }

        // GET: api/product/get-all
        [Authorize(Roles = "Admin,User")]
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response =
                await _service.GetAllProducts();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // GET: api/product/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult>
            GetById(int id)
        {
            var response =
                await _service.GetProductById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/product/add-product
        [Authorize(Roles = "Admin")]
        [HttpPost("add-product")]
        public async Task<IActionResult> AddProduct(ProductCreateDto dto)
        {
            var response =
                await _service.CreateProduct(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // PUT: api/product/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductUpdateDto dto)
        {
            var response =
                await _service.UpdateProduct(id, dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // DELETE: api/product/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response =
                await _service.DeleteProduct(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}