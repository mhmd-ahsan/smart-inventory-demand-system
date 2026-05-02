using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.CategoryDtos;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Category_Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<IEnumerable<CategoryReadDto>>> GetAllCategories();

        Task<ServiceResponse<CategoryReadDto>> GetCategoryById(int id);

        Task<ServiceResponse<int>> AddCategory(CategoryCreateDto dto);

        Task<ServiceResponse<bool>> UpdateCategory(int id, CategoryUpdateDto dto);

        Task<ServiceResponse<bool>> DeleteCategory(int id);
    }
}