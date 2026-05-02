using Microsoft.EntityFrameworkCore;
using SmartInventory.Application.Interfaces.Repo_Interfaces.Category_Interfaces;
using SmartInventory.Domain.Entities;
using SmartInventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartInventory.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        // Get All Categories
        public async Task<IEnumerable<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        // Add Categories
        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        // Add Categories
        public async Task AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
        }

        // Update Categories
        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await Task.CompletedTask;
        }

        // Delete Categories
        public async Task DeleteCategory(Category category)
        {
            _context.Categories.Remove(category);

            await Task.CompletedTask;
        }

        // Category by Name
        public async Task<bool> CategoryExistsByName(string name)
        {
            return await _context.Categories
                .AnyAsync(c => c.Name.ToLower() == name.ToLower());
        }
        // Save All Changes
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
