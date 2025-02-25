using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface ICategoriesService
    {
        Task<BaseResponse<CategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel model);
        Task<BaseResponse<CategoryResponseModel>> UpdateCategoryAsync(CreateCategoryRequestModel model, int id);
        Task<BaseResponse<CategoryResponseModel>> DeleteCategoryAsync(int id, bool status);
        Task<BaseResponse<CategoryResponseModel>> GetCategoryByIdAsync(int id);
        Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model);
    }
}
