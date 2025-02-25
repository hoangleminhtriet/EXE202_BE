using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Product;
using EunDeParfum_Service.ResponseModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IProductsService
    {
        Task<BaseResponse<ProductResponseModel>> CreateProductAsync(CreateProductRequestModel model);
        Task<BaseResponse<ProductResponseModel>> UpdateProductAsync(CreateProductRequestModel model, int id);
        Task<BaseResponse<ProductResponseModel>> DeleteProductAsync(int productId, bool status);
        Task<BaseResponse<ProductResponseModel>> GetProductById(int productId);
        Task<DynamicResponse<ProductResponseModel>> GetAllProduct(GetAllProductRequestModel model);
    }
}
