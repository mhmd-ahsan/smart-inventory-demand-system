using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartInventory.Application.DTOs.CategoryDtos;
using SmartInventory.Application.Interfaces.Service_Interfaces.Category_Interfaces;

namespace SmartInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        // GET: api/category/get-all
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAllCategories();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // GET: api/category/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetCategoryById(id);

            if (!response.Success)
                return NotFound(response);

            return Ok(response);
        }

        // POST: api/category/add-category
        [Authorize(Roles = "Admin")]
        [HttpPost("add-category")]
        public async Task<IActionResult> AddCategory(CategoryCreateDto dto)
        {
            var response = await _service.AddCategory(dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // PUT: api/category/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryUpdateDto dto)
        {
            var response = await _service.UpdateCategory(id, dto);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        // DELETE: api/category/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.DeleteCategory(id);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}