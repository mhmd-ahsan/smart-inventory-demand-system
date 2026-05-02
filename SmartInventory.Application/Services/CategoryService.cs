using AutoMapper;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.CategoryDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Category_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Category_Interfaces;
using SmartInventory.Domain.Entities;

namespace SmartInventory.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;

        public CategoryService(
            ICategoryRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get All Categories
        public async Task<ServiceResponse<IEnumerable<CategoryReadDto>>>
            GetAllCategories()
        {
            try
            {
                var categories =
                    await _repo.GetAllCategories();

                var categoryDtos =
                    _mapper.Map<IEnumerable<CategoryReadDto>>(categories);

                return ServiceResponse<IEnumerable<CategoryReadDto>>
                    .SuccessResponse(
                        categoryDtos,
                        "Categories fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<CategoryReadDto>>
                    .FailureResponse(
                        $"An error occurred while fetching categories: {ex.Message}"
                    );
            }
        }

        // Get Category By Id
        public async Task<ServiceResponse<CategoryReadDto>>
            GetCategoryById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<CategoryReadDto>
                        .FailureResponse("Invalid category id");
                }

                var category =
                    await _repo.GetCategoryById(id);

                if (category == null)
                {
                    return ServiceResponse<CategoryReadDto>
                        .FailureResponse("Category not found");
                }

                var categoryDto =
                    _mapper.Map<CategoryReadDto>(category);

                return ServiceResponse<CategoryReadDto>
                    .SuccessResponse(
                        categoryDto,
                        "Category fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<CategoryReadDto>
                    .FailureResponse(
                        $"An error occurred while fetching category: {ex.Message}"
                    );
            }
        }

        // Add Category
        public async Task<ServiceResponse<int>>
            AddCategory(CategoryCreateDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<int>
                        .FailureResponse("Category data is required");
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ServiceResponse<int>
                        .FailureResponse("Category name is required");
                }

                bool categoryExists =
                    await _repo.CategoryExistsByName(dto.Name);

                if (categoryExists)
                {
                    return ServiceResponse<int>
                        .FailureResponse("Category already exists");
                }

                var category =
                    _mapper.Map<Category>(dto);

                await _repo.AddCategory(category);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<int>
                        .FailureResponse("Failed to save category");
                }

                return ServiceResponse<int>
                    .SuccessResponse(
                        category.Id,
                        "Category created successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>
                    .FailureResponse(
                        $"An error occurred while creating category: {ex.Message}"
                    );
            }
        }

        // Update Category
        public async Task<ServiceResponse<bool>>
            UpdateCategory(int id, CategoryUpdateDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Invalid category id");
                }

                if (dto == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Category data is required");
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Category name is required");
                }

                var category =
                    await _repo.GetCategoryById(id);

                if (category == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Category not found");
                }

                bool categoryExists =
                    await _repo.CategoryExistsByName(dto.Name);

                if (categoryExists &&
                    !category.Name.Equals(
                        dto.Name,
                        StringComparison.OrdinalIgnoreCase))
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Another category with the same name already exists"
                        );
                }

                _mapper.Map(dto, category);

                await _repo.UpdateCategory(category);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Failed to update category");
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Category updated successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while updating category: {ex.Message}"
                    );
            }
        }

        // Delete Category
        public async Task<ServiceResponse<bool>>
            DeleteCategory(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Invalid category id");
                }

                var category =
                    await _repo.GetCategoryById(id);

                if (category == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Category not found");
                }

                await _repo.DeleteCategory(category);

                bool saved = await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse("Failed to delete category");
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Category deleted successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while deleting category: {ex.Message}"
                    );
            }
        }
    }
}