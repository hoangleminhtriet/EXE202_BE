using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductCategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductCategoryAsync(ProductCategory productCategory)
        {
            try
            {
                await _context.ProductCategories.AddAsync(productCategory);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory)
        {
            try
            {
                _context.ProductCategories.Update(productCategory);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteProductCategoryAsync(int productId, int categoryId)
        {
            try
            {
                var entity = await _context.ProductCategories
                    .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);

                if (entity == null)
                    return false;

                _context.ProductCategories.Remove(entity);
                return await _context.SaveChangesAsync() > 0;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ProductCategory> GetProductCategoryAsync(int productId, int categoryId)
        {
            return await _context.ProductCategories
                .FirstOrDefaultAsync(pc => pc.ProductId == productId && pc.CategoryId == categoryId);
        }

        public async Task<List<ProductCategory>> GetCategoriesByProductIdAsync(int productId)
        {
            return await _context.ProductCategories
                .Where(pc => pc.ProductId == productId)
                .ToListAsync();
        }

        public async Task<List<ProductCategory>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.ProductCategories
                .Where(pc => pc.CategoryId == categoryId)
                .ToListAsync();
        }
        public async Task<List<ProductCategory>> GetAllProductCategoriesAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }
    }
}
