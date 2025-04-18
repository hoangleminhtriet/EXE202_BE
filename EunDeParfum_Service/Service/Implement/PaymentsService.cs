using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.ResponseModel.VIETQR;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QRCoder;
using EunDeParfum_Service.Service.Interface;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Payment;
using EunDeParfum_Service.RequestModel.Payment;
using EunDeParfum_Repository.Models;
using AutoMapper;
using EunDeParfum_Repository.Repository.Interface;
using Net.payOS.Types;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class PaymentsService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;
        private readonly IOrderRepository _orderRepository;
        private readonly PayOsService _payOsService;

        public PaymentsService(IPaymentRepository paymentRepository, IMapper mapper, IOrderRepository orderRepository, PayOsService payOsService)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
            _orderRepository = orderRepository;
            _payOsService = payOsService;
        }
        public async Task<BaseResponse<PaymentResponseModel>> CreatePaymentAsync(CreatePaymentRequestModel model)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(model.OrderId);
                if (order == null || order.IsDeleted)
                {
                    return new BaseResponse<PaymentResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found or has been deleted",
                        Data = null
                    };
                }

                var payment = _mapper.Map<Payment>(model);
                payment.Amount = order.TotalAmount;
                payment.PaymentDate = DateTime.UtcNow;

                string checkoutUrl = null;
                if (model.PaymentMethod?.ToLower() == "banking")
                {
                    checkoutUrl = await GeneratePaymentLinkForOrderAsync(order.OrderId);
                    var paymentLinkId = checkoutUrl.Split('/').Last();
                    payment.TransactionId = paymentLinkId; // Lưu paymentLinkId
                    payment.Status = "Pending";
                }
                else
                {
                    payment.Status = "Paid";
                }

                await _paymentRepository.CreatePaymentAsync(payment);

                var response = _mapper.Map<PaymentResponseModel>(payment);
                response.CheckoutUrl = checkoutUrl; // Gán checkoutUrl vào response
                return new BaseResponse<PaymentResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Payment success!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<VietQrResponse> GenerateQrAsync(VietQrRequest request)
        {
            var payload = new
            {
                accountNo = request.AccountNo,
                accountName = request.AccountName,
                acqId = request.AcqId,
                amount = request.Amount,
                addInfo = request.AddInfo,
                format = "text"
            };

            using var httpClient = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.vietqr.io/v2/generate", content);
            var result = JObject.Parse(await response.Content.ReadAsStringAsync());
            var qrData = result["data"]?["qrData"]?.ToString();

            if (string.IsNullOrEmpty(qrData))
            {
                return new VietQrResponse
                {
                    Success = false,
                    Message = "Không lấy được dữ liệu QR"
                };
            }

            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(qrData, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);
            var qrBytes = qrCode.GetGraphic(20);
            string qrBase64 = Convert.ToBase64String(qrBytes);

            return new VietQrResponse
            {
                Success = true,
                QrBase64 = "data:image/png;base64," + qrBase64
            };
        }

        public async Task<DynamicResponse<PaymentResponseModel>> GetAllPaymentsAsync(GetAllPaymentRequestModel model)
        {
            try
            {
                // Lấy tất cả payments từ repository
                var listPayment = await _paymentRepository.GetAllPaymentsAsync();

                // Lọc theo Status nếu được cung cấp
                if (!string.IsNullOrWhiteSpace(model.Status))
                {
                    listPayment = listPayment.Where(p => p.Status.Equals(model.Status, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Ánh xạ sang PaymentResponseModel
                var result = _mapper.Map<List<PaymentResponseModel>>(listPayment);

                // Phân trang
                var pagePayment = result
                    .OrderBy(p => p.PaymentId)
                    .ToPagedList(model.pageNum, model.pageSize);

                return new DynamicResponse<PaymentResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Payments retrieved successfully",
                    Data = new MegaData<PaymentResponseModel>
                    {
                        PageInfo = new PagingMetaData
                        {
                            Page = pagePayment.PageNumber,
                            Size = pagePayment.PageSize,
                            Sort = "Ascending",
                            Order = "PaymentId",
                            TotalPage = pagePayment.PageCount,
                            TotalItem = pagePayment.TotalItemCount
                        },
                        PageData = pagePayment.ToList()
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<PaymentResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PaymentResponseModel>> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    return new BaseResponse<PaymentResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Payment not found",
                        Data = null
                    };
                }

                var response = _mapper.Map<PaymentResponseModel>(payment);
                return new BaseResponse<PaymentResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Payment retrieved successfully",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaymentResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<PaymentResponseModel>> GetPaymentsByStatusAsync(string status)
        {
            try
            {
                // Lấy tất cả payments từ repository
                var payments = await _paymentRepository.GetAllPaymentsAsync();

                // Lọc theo Status nếu được cung cấp
                if (!string.IsNullOrWhiteSpace(status))
                {
                    payments = payments.Where(p => p.Status.Equals(status, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                // Ánh xạ sang PaymentResponseModel
                var result = _mapper.Map<List<PaymentResponseModel>>(payments);

                return new DynamicResponse<PaymentResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Payments retrieved successfully",
                    Data = new MegaData<PaymentResponseModel>
                    {
                        PageInfo = null,
                        SearchInfo = null,
                        PageData = result
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<PaymentResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"An error occurred: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<PaymentResponseModel>> UpdatePaymentAsync(CreatePaymentRequestModel model, int paymentId)
        {
            try
            {
                var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                if (payment == null)
                {
                    return new BaseResponse<PaymentResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Payment!",
                        Data = null
                    };
                }

                _mapper.Map(model, payment);
                await _paymentRepository.UpdatePaymentAsync(payment);

                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Payment success!",
                    Data = _mapper.Map<PaymentResponseModel>(payment)
                };
            }
            catch (Exception)
            {
                return new BaseResponse<PaymentResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<string> GeneratePaymentLinkForOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null || order.IsDeleted)
            {
                throw new Exception("Order not found or has been deleted.");
            }

            // Tạo đối tượng PaymentData
            var paymentData = new PaymentData(
                orderCode: (long)order.OrderId,   // orderCode kiểu long
                amount: (int)order.TotalAmount,        // amount kiểu decimal
                description: $"Thanh toán đơn hàng #{order.OrderId}",
                items: new List<ItemData>(),       // Nếu có thể cung cấp chi tiết sản phẩm, bạn có thể bổ sung vào đây
                cancelUrl: "http://localhost:5173/payment/cancel", // Cung cấp URL hủy
                returnUrl: "http://localhost:5173/payment/success" // Cung cấp URL trả về
            );

            // Gọi dịch vụ PayOs để tạo liên kết thanh toán
            var result = await _payOsService.createPaymentLink(paymentData);

            // Trả về URL thanh toán nếu thành công
            return result.checkoutUrl;


        }
    }
}
