using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class ProductCateRepository : IProductCateRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductCateRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductCateAsync(ProductCategory productCategory)
        {
            try
            {
                await _context.ProductCategories.AddAsync(productCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteProductCateAsync(int productCateId)
        {
            try
            {
                var category = await _context.ProductCategories.FindAsync(productCateId);
                if (category == null)
                {
                    return false;
                }
                _context.ProductCategories.Remove(category);
                return await _context.SaveChangesAsync() > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ProductCategory>> GetAllProductCateAsync()
        {
            return await _context.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetProducCateByIdAsync(int id)
        {
            try
            {
                return await _context.ProductCategories.FirstOrDefaultAsync(c => c.ProductId == id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateProductCateAsync(ProductCategory productCategory)
        {
            try
            {
                _context.ProductCategories.Update(productCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
