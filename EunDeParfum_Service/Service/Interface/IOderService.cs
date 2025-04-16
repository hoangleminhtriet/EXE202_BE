using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IOderService
    {
        Task<BaseResponse<OrderReponseModel>> CreateOrderAsync(CreateOrderRequestModel model);
        Task<BaseResponse<OrderReponseModel>> UpdateOrderAsync(UpdateOrderRequestModel model, int orderId);
        Task<BaseResponse<OrderReponseModel>> DeleteOrderAsync(int orderId, bool status);
        Task<BaseResponse<OrderReponseModel>> GetOrderByIdAsync(int orderId);
        Task<DynamicResponse<OrderReponseModel>> GetAllOrdersAsync(GetAllOrderRequestModel model);
        Task<BaseResponse<OrderReponseModel>> AddToCartAsync(AddToCartRequestModel model);
        Task<BaseResponse<OrderReponseModel>> UpdateCartAsync(UpdateCartRequestModel model);

    }
}
