using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Create new ProductRecommendation
        public async Task<ProductRecommendation> CreateProductRecommendationAsync(ProductRecommendation productRecommendation)
        {
            try
            {
                await _context.ProductRecommendations.AddAsync(productRecommendation);
                await _context.SaveChangesAsync();
                return productRecommendation;
            }
            catch (Exception ex)
            {
                // Handle exception (could be logged or rethrown)
                throw new Exception("Error creating ProductRecommendation", ex);
            }
        }

        // Get list of ProductRecommendations with optional filters
        public async Task<List<ProductRecommendation>> GetAllProductRecommendationsAsync(int? productId, bool? status, int pageNum, int pageSize)
        {
            try
            {
                var query = _context.ProductRecommendations.AsQueryable();

                if (productId.HasValue)
                {
                    query = query.Where(pr => pr.ProductId == productId.Value);
                }

                if (status.HasValue)
                {
                    query = query.Where(pr => pr.Status == status.Value);
                }

                return await query
                    .Skip((pageNum - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (could be logged or rethrown)
                throw new Exception("Error retrieving ProductRecommendations", ex);
            }
        }

        // Get a single ProductRecommendation by ID
        public async Task<ProductRecommendation> GetProductRecommendationByIdAsync(int id)
        {
            try
            {
                return await _context.ProductRecommendations
                    .FirstOrDefaultAsync(pr => pr.Id == id);
            }
            catch (Exception ex)
            {
                // Handle exception (could be logged or rethrown)
                throw new Exception("Error retrieving ProductRecommendation by ID", ex);
            }
        }

        // Update an existing ProductRecommendation
        public async Task UpdateProductRecommendationAsync(ProductRecommendation productRecommendation)
        {
            try
            {
                _context.ProductRecommendations.Update(productRecommendation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exception (could be logged or rethrown)
                throw new Exception("Error updating ProductRecommendation", ex);
            }
        }

        // Delete ProductRecommendation by ID
        public async Task DeleteProductRecommendationAsync(int id)
        {
            try
            {
                var productRecommendation = await _context.ProductRecommendations
                    .FirstOrDefaultAsync(pr => pr.Id == id);

                if (productRecommendation != null)
                {
                    _context.ProductRecommendations.Remove(productRecommendation);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("ProductRecommendation not found");
                }
            }
            catch (Exception ex)
            {
                // Handle exception (could be logged or rethrown)
                throw new Exception("Error deleting ProductRecommendation", ex);
            }
        }
    }
}
