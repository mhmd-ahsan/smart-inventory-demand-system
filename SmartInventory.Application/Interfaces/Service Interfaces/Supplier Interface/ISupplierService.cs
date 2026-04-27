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
        Task<IEnumerable<SupplierReadDto>> GetAllSupplier();
        Task<SupplierReadDto?> GetSupplierById(int id);
        Task AddSupplier(CreateSupplierDto dto);
        Task UpdateSupplier(int id, UpdateSupplierDto dto);
        Task DeleteSupplier(int id);
    }
}
