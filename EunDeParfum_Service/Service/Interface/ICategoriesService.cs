using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EunDeParfum_Service.RequestModel;
using EunDeParfum_Service.ResponseModel;

namespace EunDeParfum_Service.Service.Interface
{
    public interface ICategoriesService
    {
        Task<BaseResponse<CategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel model);
        Task<BaseResponse<CategoryResponseModel>> UpdateCategoryAsync(CreateCategoryRequestModel model, int id);
        Task<BaseResponse<CategoryResponseModel>> DeleteCategoryAsync(int categoryId, bool status);
        Task<BaseResponse<CategoryResponseModel>> GetCategoryByIdAsync(int categoryId);
        Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model);
    }
}
