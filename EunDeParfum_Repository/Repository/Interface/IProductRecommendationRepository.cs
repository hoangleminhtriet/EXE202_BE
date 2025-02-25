using EunDeParfum_Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IProductRecommendationRepository
    {
        Task<bool> CreateProductRecommendationAsync(ProductRecommendation productRecommendation);
        Task<bool> UpdateProductRecommendationAsync(ProductRecommendation productRecommendation);
        Task<bool> DeleteProductRecommendationAsync(int id);
        Task<ProductRecommendation> GetProductRecommendationByIdAsync(int id);
        Task<List<ProductRecommendation>> GetAllProductRecommendationsAsync();
    }
}
