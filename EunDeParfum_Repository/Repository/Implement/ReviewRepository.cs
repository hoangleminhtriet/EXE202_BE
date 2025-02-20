using EunDeParfum_Repository.DbContexts;
using EunDeParfum_Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Implement
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;
        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateReviewAsync(Review review)
        {
            try
            {
                _context.Reviews.AddAsync(review);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteReviewAsync(int reviewId)
        {
            try
            {
                var review = await _context.Reviews.FindAsync(reviewId);
                if(review == null)
                {
                    return false;
                }
                _context.Reviews.Remove(review);
                return await _context.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            return await _context.Reviews.ToListAsync();
        }

        public Task<Review> GetReviewByIdAsync(int id)
        {
            try
            {
                return _context.Reviews.FirstOrDefaultAsync(r => r.ReviewId == id);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public async Task<bool> UpdateReviewAsync(Review review)
        {
            try
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
