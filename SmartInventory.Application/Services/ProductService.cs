using AutoMapper;
using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface;
using SmartInventory.Domain.Entities;

namespace SmartInventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;

        public ProductService(
            IProductRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get All Products
        public async Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetAllProducts()
        {
            try
            {
                var products =
                    await _repo.GetAllProductsAsync();

                var productDtos =
                    _mapper.Map<
                        IEnumerable<ProductReadDto>>
                        (products);

                return ServiceResponse
                    <IEnumerable<ProductReadDto>>
                    .SuccessResponse(
                        productDtos,
                        "Products fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <IEnumerable<ProductReadDto>>
                    .FailureResponse(
                        $"An error occurred while fetching products: {ex.Message}"
                    );
            }
        }

        // Get Product By Id
        public async Task<ServiceResponse<ProductReadDto>> GetProductById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse
                        <ProductReadDto>
                        .FailureResponse(
                            "Invalid product id"
                        );
                }

                var product =
                    await _repo.GetByIdAsync(id);

                if (product == null)
                {
                    return ServiceResponse
                        <ProductReadDto>
                        .FailureResponse(
                            "Product not found"
                        );
                }

                var productDto =
                    _mapper.Map<ProductReadDto>
                    (product);

                return ServiceResponse
                    <ProductReadDto>
                    .SuccessResponse(
                        productDto,
                        "Product fetched successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse
                    <ProductReadDto>
                    .FailureResponse(
                        $"An error occurred while fetching product: {ex.Message}"
                    );
            }
        }

        // Add Product
        public async Task<ServiceResponse<int>> CreateProduct(ProductCreateDto dto)
        {
            try
            {
                if (dto == null)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Product data is required"
                        );
                }

                if (string.IsNullOrWhiteSpace(dto.Name))
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Product name is required"
                        );
                }

                var product =
                    _mapper.Map<Product>(dto);

                await _repo.AddProductAsync(product);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<int>
                        .FailureResponse(
                            "Failed to save product"
                        );
                }

                return ServiceResponse<int>
                    .SuccessResponse(
                        product.Id,
                        "Product created successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<int>
                    .FailureResponse(
                        $"An error occurred while creating product: {ex.Message}"
                    );
            }
        }

        // Update Product
        public async Task<ServiceResponse<bool>> UpdateProduct(int id, ProductUpdateDto dto)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid product id"
                        );
                }

                if (dto == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Product data is required"
                        );
                }

                var product =
                    await _repo.GetByIdAsync(id);

                if (product == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Product not found"
                        );
                }

                _mapper.Map(dto, product);

                _repo.UpdateProductAsync(product);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to update product"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Product updated successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while updating product: {ex.Message}"
                    );
            }
        }

        // Delete Product
        public async Task<ServiceResponse<bool>> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Invalid product id"
                        );
                }

                var product =
                    await _repo.GetByIdAsync(id);

                if (product == null)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Product not found"
                        );
                }

                _repo.DeleteProductAsync(product);

                bool saved =
                    await _repo.SaveChangesAsync();

                if (!saved)
                {
                    return ServiceResponse<bool>
                        .FailureResponse(
                            "Failed to delete product"
                        );
                }

                return ServiceResponse<bool>
                    .SuccessResponse(
                        true,
                        "Product deleted successfully"
                    );
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>
                    .FailureResponse(
                        $"An error occurred while deleting product: {ex.Message}"
                    );
            }
        }
    }
}