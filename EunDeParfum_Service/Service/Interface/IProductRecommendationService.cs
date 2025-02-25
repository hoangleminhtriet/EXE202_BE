using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductRecoment;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IProductRecommendationService
    {
        Task<BaseResponse<ProductRecommendationResponseModel>> CreateProductRecommendationAsync(CreateProductRecommendationRequestModel model);
        Task<BaseResponse<ProductRecommendationResponseModel>> UpdateProductRecommendationAsync(UpdateProductRecommendationRequestModel model, int id);
        Task<BaseResponse<ProductRecommendationResponseModel>> DeleteProductRecommendationAsync(int id, bool status);
        Task<BaseResponse<ProductRecommendationResponseModel>> GetProductRecommendationByIdAsync(int id);
        Task<DynamicResponse<ProductRecommendationResponseModel>> GetAllProductRecommendationsAsync(GetAllProductRecommendationRequestModel model);
    }
}
