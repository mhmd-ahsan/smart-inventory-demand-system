using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        //[Authorize]
        // Get All Products
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllProducts();

            return Ok(new ApiResponse<IEnumerable<ProductReadDto>>
            {
                Success = true,
                Message = "Products fetched",
                Data = result
            });
        }

        // Get Product by Id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _service.GetProductById(id);

            if (product is null)
            {
                return NotFound(
                    new ApiResponse<string>
                    {
                        Success = false,
                        Message = "Product not found",
                        Data = null
                    });
            }

            return Ok(new ApiResponse<ProductReadDto>
            {
                Success = true,
                Message = "Product Found",
                Data = product
            });
        }

        // Add Product
        [HttpPost("Add-product")]
        public async Task<IActionResult> AddProduct([FromBody] ProductCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _service.CreateProduct(dto);
            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product created successfully",
                Data = "Created"
            });
        }

        // Update Product
        [HttpPut("{id:int}")] 
        public async Task<IActionResult> UpdateProduct(int id,[FromBody] ProductUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            await _service.UpdateProduct(id, dto);

            return Ok(new ApiResponse<string>
            {
                Success = true,
                Message = "Product updated successfully",
                Data = "Updated"
            });
        }

        // Delete Product
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _service.DeleteProduct(id);

            return Ok(
                new ApiResponse<string>
                {
                    Success = true,
                    Message = "Product deleted successfully",
                    Data = "Deleted"
                });
        }
    }
}
