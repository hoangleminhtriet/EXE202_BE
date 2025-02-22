using EunDeParfum_Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IProductRepository
    {
        Task<bool> CreateProductAsync(Review review);
        Task<bool> UpdateProductAsync(Review review);
        Task<bool> DeleteProductAsync(int reviewId);
        Task<Review> GetProductByIdAsync(int id);
        Task<List<Review>> GetAllProductAsync();
        Task CreateProductAsync(Product product);
    }
}
