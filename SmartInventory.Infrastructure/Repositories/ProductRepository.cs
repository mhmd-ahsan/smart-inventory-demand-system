using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.Interfaces.Product_Interfaces;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Products With Supplier
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .ToListAsync();
        }

        // Get Product By Id
        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Supplier)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        // Add Product
        public async Task AddProductAsync(Product product)
        {
             await _context.Products.AddAsync(product);
        }

        // Update Product
        public  void UpdateProductAsync(Product product)
        {
             _context.Products.Update(product);
        }

        // Delete Product
        public  void DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
        }

        // Save Changes for the  Product
        public async Task SaveChangesAsync()
        {
           await  _context.SaveChangesAsync();
        }
    }
}
