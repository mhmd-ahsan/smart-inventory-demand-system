using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.SaleDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Sales_Interface
{
    public interface ISaleService
    {
        Task<ServiceResponse<IEnumerable<SaleReadDto>>> GetAllSales();
        Task<ServiceResponse<SaleReadDto>> GetSaleById(int id);
        Task<ServiceResponse<int>> CreateSale(CreateSaleDto dto);
        Task<ServiceResponse<bool>> UpdateSale(int id, UpdateSaleDto dto);
        Task<ServiceResponse<bool>> DeleteSale(int id);
    }
}
