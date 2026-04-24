using SmartInventory.Application.DTOs.ProductsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Product_Interface
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProducts();
        Task<ProductReadDto?> GetProductById(int id);   
        Task CreateProduct(ProductCreateDto dto);
        Task UpdateProduct(int id, ProductUpdateDto dto);
        Task DeleteProduct(int id);
    }
}
