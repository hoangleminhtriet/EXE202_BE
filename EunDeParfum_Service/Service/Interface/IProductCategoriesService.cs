using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.RequestModel.ProductCate;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Product;
using EunDeParfum_Service.ResponseModel.ProductCate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IProductCategoriesService
    {

        Task<BaseResponse<ProductCateResponseModel>> CreateProductCateAsync(CreateProductCateRequestModel model);
        Task<BaseResponse<ProductCateResponseModel>> UpdateProductCateAsync(CreateProductCateRequestModel model, int id);
        Task<BaseResponse<ProductCateResponseModel>> DeleteProductCateAsync(int productCateId, bool status);
        Task<BaseResponse<ProductCateResponseModel>> GetProductCateById(int reviewId);
        Task<DynamicResponse<ProductCateResponseModel>> GetAllProductCate(GetAllProductCateRequestModel model);
    }
}
