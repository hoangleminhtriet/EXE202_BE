using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetailResponseModel>> CreateListOrderDetails(CreateOrderDetailRequestModel model);
        Task<BaseResponse<OrderDetailResponseModel>> GetOrderDetailByIdAsync(int orderDetailId);
        Task<List<OrderDetailResponseModel>> GetListOrderDetailsByOrderId(int orderId);
        Task<List<OrderDetailResponseModel>> GetListOrderDetailsByListOrderIds(List<int> orderIds);
        Task<BaseResponse<OrderDetailResponseModel>> UpdateOrderDetailAsync(UpdateOrderDetailRequestModel model);
        Task<BaseResponse<bool>> DeleteOrderDetailAsync(int orderDetailId);
        Task<BaseResponse<bool>> RemoveOrderDetailsAsync(int orderId, List<int> orderDetailIds);
    }
}
