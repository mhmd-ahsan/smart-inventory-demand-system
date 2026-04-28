using AutoMapper;
using SmartInventory.Application.DTOs.ProductsDtos;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface;
using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // Get All products
        public async Task<IEnumerable<ProductReadDto>> GetAllProducts()
        {
            var products = await _repo.GetAllProductsAsync();

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        // Get Product By Id
        public async Task<ProductReadDto?> GetProductById(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product is null)
                return null;

            return _mapper.Map<ProductReadDto>(product);
        }

        // Add Product
        public async Task CreateProduct(ProductCreateDto dto)
        {
            var product =  _mapper.Map<Product>(dto);

            await _repo.AddProductAsync(product);
            await _repo.SaveChangesAsync();
        }

        // Update Product
        public async Task UpdateProduct(int id, ProductUpdateDto dto)
        {
            var product = await _repo.GetByIdAsync(id);

            if (product is null)
                throw new Exception(
                "Product Not Found");

            _mapper.Map(dto, product);
             _repo.UpdateProductAsync(product);

            await _repo.SaveChangesAsync();
        }

        // Delete Product
        public async Task DeleteProduct(int id)
        {
            var product = await _repo.GetByIdAsync(id);

            if(product is null)
                throw new Exception(
                "Product Not Found");
            
            _repo.DeleteProductAsync(product);
            await _repo.SaveChangesAsync();
        }
    }
}
