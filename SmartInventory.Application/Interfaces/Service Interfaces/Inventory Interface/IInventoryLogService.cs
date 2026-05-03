using SmartInventory.Application.Common.Responses;
using SmartInventory.Application.DTOs.InventoryLogDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Service_Interfaces.Inventory_Interface
{
    public interface IInventoryLogService
    {
        Task<ServiceResponse<IEnumerable<InventoryLogReadDto>>> GetAllAsync();
        Task<ServiceResponse<InventoryLogReadDto>> GetByIdAsync(int id);
        Task<ServiceResponse<int>> CreateAsync(CreateInventoryLogDto dto);
        Task<ServiceResponse<bool>> UpdateAsync(int id, UpdateInventoryLogDto dto);
        Task<ServiceResponse<bool>> DeleteAsync(int id);
    }
}
