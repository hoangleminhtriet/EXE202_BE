using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.ResponseModel.ProductRecommendation;

public interface IProductRecommendationService
{
    Task<ProductRecommendationResponseModel> CreateProductRecommendationAsync(CreateProductRecommendationRequestModel model);
    Task<List<ProductRecommendationResponseModel>> GetAllProductRecommendationsAsync(int? productId, bool? status, int pageNum, int pageSize);  // Chỉnh sửa ở đây
    Task<ProductRecommendationResponseModel> GetProductRecommendationByIdAsync(int id);
    Task UpdateProductRecommendationAsync(CreateProductRecommendationRequestModel model, int id);
    Task DeleteProductRecommendationAsync(int id);
}
