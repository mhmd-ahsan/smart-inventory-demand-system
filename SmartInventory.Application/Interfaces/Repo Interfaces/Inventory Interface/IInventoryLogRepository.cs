using SmartInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Application.Interfaces.Repo_Interfaces.Inventory_Interface
{
    public interface IInventoryLogRepository
    {
        Task<IEnumerable<InventoryLog>>GetAllInventoryLogAsync();
        Task<InventoryLog?>GetByIdAsync(int id);
        Task AddInventoryAsync(InventoryLog log);
        void UpdateInventory(InventoryLog log);
        void DeleteInventory(InventoryLog log);
        Task<bool> SaveChangesAsync();
    }
}
