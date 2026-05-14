using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.DTOs.DashboardDtos;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Dashboard_Interface;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AppDbContext _db;

        public DashboardRepository(AppDbContext db)
        {
            _db = db;
        }
        
        public async Task<DashboardReadDto> GetDashboardDataAsync()
        {
            var totalProducts = await _db.Products.CountAsync();

            var totalCategories = await _db.Categories.CountAsync();

            var totalSuppliers = await _db.Suppliers.CountAsync();

            var totalSales = await _db.Sales.CountAsync();

            var totalRevenue = await _db.Sales.SumAsync(s => s.TotalPrice);

            var lowStockProducts = await _db.Products.CountAsync(p => p.StockQuantity < 10);

            var topSellingProducts = await _db.Sales.Include(s => s.Product)
                .GroupBy(s => s.Product.Name)
                .Select(g => new
                {
                    ProductName = g.Key,
                    TotalSold = g.Sum(x => x.Quantity)
                })
                .OrderByDescending(x => x.TotalSold)
                .Take(5)
                .Select(x => x.ProductName)
                .ToListAsync();

            return new DashboardReadDto
            {
                TotalProducts = totalProducts,
                TotalCategories = totalCategories,
                TotalSuppliers = totalSuppliers,
                TotalSales = totalSales,
                TotalRevenue = totalRevenue,
                LowStockProducts = lowStockProducts,
                TopSellingProducts = topSellingProducts,
            };
        }

        public async Task<List<DemandAnalysisDto>> GetDemandAnalysisAsync()
        {
            var last30Days = DateTime.UtcNow.AddDays(-30);

            var data = await _db.Sales
                .Where(s => s.SaleDate >= last30Days)
                .Include(s => s.Product)
                .GroupBy(s => new { s.ProductId, s.Product.Name, s.Product.StockQuantity })
                .Select(g => new DemandAnalysisDto
                {
                    ProductName = g.Key.Name,
                    TotalSold = g.Sum(x => x.Quantity),
                    AvgSalesPerDay = g.Sum(x => x.Quantity) / 30.0,
                    CurrentStock = g.Key.StockQuantity,
                    NeedsRestock = g.Key.StockQuantity < (g.Sum(x => x.Quantity) / 30.0) * 7 // next 7 days demand
                })
                .OrderByDescending(x => x.TotalSold)
                .ToListAsync();

            return data;
        }
    }
}
