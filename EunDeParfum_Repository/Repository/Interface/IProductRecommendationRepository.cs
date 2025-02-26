using EunDeParfum_Repository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Repository.Interface
{
    public interface IProductRecommendationRepository
    {
        Task<ProductRecommendation> CreateProductRecommendationAsync(ProductRecommendation productRecommendation);
        Task<List<ProductRecommendation>> GetAllProductRecommendationsAsync(int? productId, bool? status, int pageNum, int pageSize);
        Task<ProductRecommendation> GetProductRecommendationByIdAsync(int id);
        Task UpdateProductRecommendationAsync(ProductRecommendation productRecommendation);
        Task DeleteProductRecommendationAsync(int id);
    }
}
