using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Sale_Interface;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly AppDbContext _context;
        public SaleRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Sales
        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            return await _context.Sales.Include(s => s.Product)
                .ToListAsync();
        }

        // Get By Id
        public async Task<Sale?> GetSaleByIdAsync(int id)
        {
            return await _context.Sales.Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        // Add Sale
        public async Task AddSaleAsync(Sale sale)
        {
            await _context.Sales.AddAsync(sale);
        }

        // Update Sale
        public void UpdateSale(Sale sale)
        {
            _context.Sales.Update(sale);
        }

        // Delete Sale
        public void DeleteSale(Sale sale)
        {
            _context.Sales.Remove(sale);
        }

        // Save 
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
