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
        Task<bool> CreateProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
        Task<Product> GetProductByIdAsync(int id);
        Task<List<Product>> GetProductsByIdsAsync(List<int> productIds);
        Task<List<Product>> GetAllProductAsync();
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
    }
}
