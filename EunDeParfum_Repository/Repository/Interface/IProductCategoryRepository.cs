using EunDeParfum_Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IProductCategoryRepository
    {
        Task<bool> CreateProductCategoryAsync(ProductCategory productCategory);
        Task<bool> UpdateProductCategoryAsync(ProductCategory productCategory);
        Task<bool> DeleteProductCategoryAsync(int productId, int categoryId);
        Task<ProductCategory> GetProductCategoryAsync(int productId, int categoryId);
        Task<List<ProductCategory>> GetCategoriesByProductIdAsync(int productId);
        Task<List<ProductCategory>> GetProductsByCategoryIdAsync(int categoryId);
        Task<List<ProductCategory>> GetAllProductCategoriesAsync();
    }
}
