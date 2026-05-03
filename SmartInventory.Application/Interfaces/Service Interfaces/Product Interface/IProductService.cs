using SmartInventory.Application.Common.Responses;
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
        Task<ServiceResponse<IEnumerable<ProductReadDto>>> GetAllProducts();
        Task<ServiceResponse<ProductReadDto>> GetProductById(int id);
        Task<ServiceResponse<int>> CreateProduct(ProductCreateDto dto);
        Task<ServiceResponse<bool>> UpdateProduct(int id, ProductUpdateDto dto);
        Task<ServiceResponse<bool>> DeleteProduct(int id);
    }
}
