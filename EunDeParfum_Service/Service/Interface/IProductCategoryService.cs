using EunDeParfum_Service.RequestModel.ProductCate;
using EunDeParfum_Service.RequestModel.ProductCategory;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductCategory;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IProductCategoryService
    {
        Task<BaseResponse<ProductCategoryResponseModel>> CreateProductCategoryAsync(CreateProductCategoryRequestModel model);
        Task<BaseResponse<ProductCategoryResponseModel>> UpdateProductCategoryAsync(UpdateProductCategoryRequestModel model);
        Task<BaseResponse<bool>> DeleteProductCategoryAsync(DeleteProductCategoryRequestModel model);
        Task<BaseResponse<ProductCategoryResponseModel>> GetProductCategoryAsync(int productId, int categoryId);
        Task<DynamicResponse<ProductCategoryResponseModel>> GetCategoriesByProductIdAsync(int productId);
        Task<DynamicResponse<ProductCategoryResponseModel>> GetProductsByCategoryIdAsync(int categoryId);
        Task<DynamicResponse<ProductCategoryResponseModel>> GetAllProductCategoriesAsync(GetAllProductCategoriesRequestModel model);

    }
}
