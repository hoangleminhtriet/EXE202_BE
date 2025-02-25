using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class ProductRecommendationRepository : IProductRecommendationRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRecommendationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateProductRecommendationAsync(ProductRecommendation productRecommendation)
        {
            try
            {
                await _context.ProductRecommendations.AddAsync(productRecommendation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating product recommendation", ex);
            }
        }

        public async Task<bool> UpdateProductRecommendationAsync(ProductRecommendation productRecommendation)
        {
            try
            {
                _context.ProductRecommendations.Update(productRecommendation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating product recommendation", ex);
            }
        }

        public async Task<bool> DeleteProductRecommendationAsync(int id)
        {
            try
            {
                var productRecommendation = await _context.ProductRecommendations.FindAsync(id);
                if (productRecommendation == null)
                    return false;

                _context.ProductRecommendations.Remove(productRecommendation);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting product recommendation", ex);
            }
        }

        public async Task<ProductRecommendation> GetProductRecommendationByIdAsync(int id)
        {
            try
            {
                return await _context.ProductRecommendations.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving product recommendation by ID", ex);
            }
        }

        public async Task<List<ProductRecommendation>> GetAllProductRecommendationsAsync()
        {
            try
            {
                return await _context.ProductRecommendations.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all product recommendations", ex);
            }
        }
    }
}
