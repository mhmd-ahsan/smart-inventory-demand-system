using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Inventory_Interface;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class InventoryLogRepository : IInventoryLogRepository
    {
        private readonly AppDbContext _db;

        public InventoryLogRepository(AppDbContext db)
        {
            _db = db;
        }

        // Get All Products
        public async Task<IEnumerable<InventoryLog>> GetAllInventoryLogAsync()
        {
            return await _db.InventoryLogs.Include(x => x.Product)
                .AsNoTracking()
                .ToListAsync();
        }

        // Get by Id 
        public async Task<InventoryLog?> GetByIdAsync(int id)
        {
            return await _db.InventoryLogs
                .Include(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);    
        }

        // Add InventoryLog
        public async Task AddInventoryAsync(InventoryLog log)
        {
             await _db.InventoryLogs.AddAsync(log);
        }

        // Update InventoryLog
        public  async void UpdateInventory(InventoryLog log)
        {
             _db.InventoryLogs.Update(log);
        }


        // Delete InventoryLog
        public async void  DeleteInventory(InventoryLog log)
        {
            _db.InventoryLogs.Remove(log);
        }

        // SaveChangesAsync
        public async Task<bool> SaveChangesAsync()
        {
            return await _db.SaveChangesAsync() > 0;
        }
    }
}
