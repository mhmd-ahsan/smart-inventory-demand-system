using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.Interfaces.Repo_Interfaces;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _db;

        public SupplierRepository(AppDbContext db)
        {
            _db = db;
        }

        // Get All Suppliers
        public async Task<IEnumerable<Supplier>> GetAllSuppliersAsync()
        {
            return await _db.Suppliers.AsNoTracking().ToListAsync();
        }

        // Get By Id 
        public async Task<Supplier?> GetSupplierByIdAsyn(int id)
        {
            return await _db.Suppliers.FirstOrDefaultAsync(s => s.Id == id);
        }

        // Add Supplier
        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _db.Suppliers.AddAsync(supplier);
        }

        // Update Supplier
        public async void UpdateSupplier(Supplier supplier)
        {
             _db.Suppliers.Update(supplier);
        }

        // Delete Supplier
        public async void RemoveSupplier(Supplier supplier)
        {
            _db.Suppliers.Remove(supplier);
        }

        // Save All Changes
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
