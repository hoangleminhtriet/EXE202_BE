using EunDeParfum_Service.RequestModel.Payment;
using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Payment;
using EunDeParfum_Service.ResponseModel.VIETQR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IPaymentService
    {
        Task<VietQrResponse> GenerateQrAsync(VietQrRequest request);
        Task<BaseResponse<PaymentResponseModel>> CreatePaymentAsync(CreatePaymentRequestModel model);
        Task<BaseResponse<PaymentResponseModel>> UpdatePaymentAsync(CreatePaymentRequestModel model, int paymentId);
        Task<BaseResponse<PaymentResponseModel>> GetPaymentByIdAsync(int paymentId);
        Task<DynamicResponse<PaymentResponseModel>> GetAllPaymentsAsync(GetAllPaymentRequestModel model);
        Task<DynamicResponse<PaymentResponseModel>> GetPaymentsByStatusAsync(string status);
    }
}
