using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.SupplierDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Supplier_Interface
{
    public interface ISupplierService
    {
        Task<ServiceResponse<IEnumerable<SupplierReadDto>>> GetAllSupplier();
        Task<ServiceResponse<SupplierReadDto>> GetSupplierById(int id);
        Task<ServiceResponse<int>> AddSupplier(CreateSupplierDto dto);
        Task<ServiceResponse<bool>> UpdateSupplier(int id, UpdateSupplierDto dto);
        Task<ServiceResponse<bool>> DeleteSupplier(int id);
    }
}
