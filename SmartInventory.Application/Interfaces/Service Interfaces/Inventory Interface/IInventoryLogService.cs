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
        Task<IEnumerable<InventoryLogReadDto>> GetAllAsync();
        Task<InventoryLogReadDto>GetByIdAsync(int id);
        Task CreateAsync(CreateInventoryLogDto dto);
        Task UpdateAsync(int id,UpdateInventoryLogDto dto);
        Task DeleteAsync(int id);
    }
}
