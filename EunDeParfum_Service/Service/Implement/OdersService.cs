using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Order;
using EunDeParfum_Service.ResponseModel.OrderDetail;
using EunDeParfum_Service.Service.Interface;
using Net.payOS.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Implement
{
    public class OdersService : IOderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IOrderDetailService _orderDetailService;
        private readonly PayOsService _payOsService;

        public OdersService(IOrderRepository orderRepository, IMapper mapper, IOrderDetailService orderDetailService, PayOsService payOsService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderDetailService = orderDetailService;
            _payOsService = payOsService;
        }

        public async Task<BaseResponse<OrderReponseModel>> AddToCartAsync(AddToCartRequestModel model)
        {
            try
            {
                // 1. Kiểm tra xem khách hàng đã có đơn hàng trạng thái "Cart" hay chưa
                var existingCart = (await _orderRepository.GetAllOrdersAsync())
                    .FirstOrDefault(o => o.CustomerId == model.CustomerId && o.Status == "Cart" && !o.IsDeleted);

                Order order;
                if (existingCart == null)
                {
                    // 2. Nếu chưa có giỏ hàng, tạo mới đơn hàng với trạng thái "Cart"
                    order = new Order
                    {
                        CustomerId = model.CustomerId,
                        TotalAmount = 0,
                        OrderDate = DateTime.UtcNow,
                        Status = "Cart",
                        IsDeleted = false
                    };
                    await _orderRepository.CreateOrderAsync(order);
                }
                else
                {
                    // 3. Nếu đã có giỏ hàng, sử dụng đơn hàng hiện tại
                    order = existingCart;
                }

                // 4. Lấy danh sách OrderDetails hiện tại của đơn hàng
                var currentOrderDetails = await _orderDetailService.GetListOrderDetailsByOrderId(order.OrderId);
                currentOrderDetails ??= new List<OrderDetailResponseModel>();

                // 5. Chuẩn bị danh sách sản phẩm để thêm hoặc cập nhật
                var orderDetailsToAdd = new List<ProductForOrderRequestModel>();

                foreach (var newProduct in model.Products)
                {
                    // Kiểm tra xem sản phẩm đã tồn tại trong OrderDetails hay chưa
                    var existingDetail = currentOrderDetails.FirstOrDefault(od => od.ProductId == newProduct.ProductId);

                    if (existingDetail != null)
                    {
                        // Nếu sản phẩm đã tồn tại, cập nhật số lượng
                        var updateModel = new UpdateOrderDetailRequestModel
                        {
                            OrderDetailId = existingDetail.OrderDetailId,
                            Quantity = existingDetail.Quantity + newProduct.Quantity,
                            Price = newProduct.Price
                        };
                        var updateResult = await _orderDetailService.UpdateOrderDetailAsync(updateModel);
                        if (updateResult == null || !updateResult.Success)
                        {
                            return new BaseResponse<OrderReponseModel>
                            {
                                Code = 500,
                                Success = false,
                                Message = "Không thể cập nhật chi tiết đơn hàng!",
                                Data = null
                            };
                        }
                        // Cập nhật lại currentOrderDetails với thông tin mới
                        existingDetail.Quantity = updateModel.Quantity;
                        existingDetail.UnitPrice = updateModel.Price;
                    }
                    else
                    {
                        // Nếu sản phẩm chưa tồn tại, thêm mới vào danh sách
                        orderDetailsToAdd.Add(new ProductForOrderRequestModel
                        {
                            ProductId = newProduct.ProductId,
                            Quantity = newProduct.Quantity,
                            Price = newProduct.Price
                        });
                    }
                }

                // 6. Nếu có sản phẩm mới, thêm vào OrderDetails
                if (orderDetailsToAdd.Any())
                {
                    var createOrderDetails = new CreateOrderDetailRequestModel
                    {
                        OrderId = order.OrderId,
                        Products = orderDetailsToAdd
                    };
                    var createResult = await _orderDetailService.CreateListOrderDetails(createOrderDetails);
                    if (createResult == null || !createResult.Any())
                    {
                        return new BaseResponse<OrderReponseModel>
                        {
                            Code = 500,
                            Success = false,
                            Message = "Không thể tạo chi tiết đơn hàng!",
                            Data = null
                        };
                    }

                    // Cập nhật lại danh sách OrderDetails sau khi tạo mới
                    currentOrderDetails.AddRange(createResult);
                }

                // 7. Tính lại totalAmount dựa trên tất cả OrderDetails
                decimal totalAmount = currentOrderDetails.Sum(od => od.Quantity * od.UnitPrice);
                order.TotalAmount = totalAmount;
                await _orderRepository.UpdateOrderAsync(order);

                // 8. Chuẩn bị phản hồi
                var response = _mapper.Map<OrderReponseModel>(order);
                response.OrderDetails = currentOrderDetails;

                return new BaseResponse<OrderReponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Thêm sản phẩm vào giỏ hàng thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderReponseModel>> CreateOrderAsync(CreateOrderRequestModel model)
        {
            try
            {
                var order = _mapper.Map<Order>(model);
                decimal totalAmout = 0;
                foreach(var product in model.Products)
                {
                    totalAmout += product.Price * product.Quantity;
                }
                order.TotalAmount = totalAmout;
                order.OrderDate = DateTime.UtcNow;
                order.IsDeleted = false;
                await _orderRepository.CreateOrderAsync(order);

                var orderId = order.OrderId;
                var orderDetails = model.Products;
                var createOrderDetails = new CreateOrderDetailRequestModel
                {
                    OrderId = orderId,
                    Products = orderDetails
                };
                var orderDetailResponse = await _orderDetailService.CreateListOrderDetails(createOrderDetails);

                var response = _mapper.Map<OrderReponseModel>(order);
                response.OrderDetails = orderDetailResponse;
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Order success!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderReponseModel>> DeleteOrderAsync(int orderId, bool status)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<OrderReponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Order not found!",
                        Data = null
                    };
                }

                order.IsDeleted = status;
                await _orderRepository.UpdateOrderAsync(order);

                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Delete Order success!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message,
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

        public async Task<DynamicResponse<OrderReponseModel>> GetAllOrdersAsync(GetAllOrderRequestModel model)
        {
            try
            {
                // 1️⃣ Lấy danh sách đơn hàng từ DB theo điều kiện tìm kiếm
                var orders = await _orderRepository.GetAllOrdersAsync();

                // 2️⃣ Kiểm tra dữ liệu
                if (!orders.Any())
                {
                    return new DynamicResponse<OrderReponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "No orders found!",
                        Data = null
                    };
                }

                // 3️⃣ Phân trang
                var pageOrders = orders
                    .OrderByDescending(o => o.OrderDate) // Sắp xếp mới nhất
                    .Skip((model.pageNum - 1) * model.pageSize) // Bỏ qua các bản ghi trước đó
                    .Take(model.pageSize) // Lấy số lượng theo pageSize
                    .ToList();

                var totalItemCount = orders.Count();
                var totalPages = (int)Math.Ceiling((double)totalItemCount / model.pageSize);

                // 4️⃣ Lấy toàn bộ OrderDetails theo danh sách OrderId
                var orderIds = pageOrders.Select(o => o.OrderId).ToList(); // Chỉ lấy OrderDetails cho các đơn hàng trong trang hiện tại
                var allOrderDetails = await _orderDetailService.GetListOrderDetailsByListOrderIds(orderIds);

                // 5️⃣ Chuyển OrderDetails thành Dictionary (Key: OrderId, Value: List<OrderDetailResponseModel>)
                var orderDetailsDict = allOrderDetails
                    .GroupBy(od => od.OrderId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // 6️⃣ Chuyển dữ liệu sang List<OrderReponseModel> bằng AutoMapper
                var responseList = pageOrders.Select(order =>
                {
                    var response = _mapper.Map<OrderReponseModel>(order);
                    response.OrderDetails = orderDetailsDict.ContainsKey(order.OrderId)
                        ? orderDetailsDict[order.OrderId]
                        : new List<OrderDetailResponseModel>();
                    return response;
                }).ToList();

                // 7️⃣ Tạo MegaData<OrderResponseModel>
                var responseData = new MegaData<OrderReponseModel>()
                {
                    PageInfo = new PagingMetaData()
                    {
                        Page = model.pageNum,
                        Size = model.pageSize,
                        Sort = "Descending",
                        Order = "OrderDate",
                        TotalPage = totalPages,
                        TotalItem = totalItemCount
                    },
                    SearchInfo = new SearchCondition()
                    {
                        keyWord = null,
                        role = null,
                        status = null,
                        is_Verify = null,
                        is_Delete = null
                    },
                    PageData = responseList
                };

                return new DynamicResponse<OrderReponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Get all orders success!",
                    Data = responseData
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<OrderReponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }


        public async Task<BaseResponse<OrderReponseModel>> GetOrderByIdAsync(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                var response = _mapper.Map<OrderReponseModel>(order);
                response.OrderDetails = await _orderDetailService.GetListOrderDetailsByOrderId(orderId);
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Order success!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderReponseModel>> UpdateCartAsync(UpdateCartRequestModel model)
        {
            try
            {
                // 1. Kiểm tra xem khách hàng đã có đơn hàng trạng thái "Cart" hay chưa
                var cart = (await _orderRepository.GetAllOrdersAsync())
                    .FirstOrDefault(o => o.CustomerId == model.CustomerId && o.Status == "Cart" && !o.IsDeleted);

                if (cart == null)
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Không tìm thấy giỏ hàng cho khách hàng này!",
                        Data = null
                    };
                }

                // 2. Lấy danh sách OrderDetails hiện tại của giỏ hàng
                var currentOrderDetails = await _orderDetailService.GetListOrderDetailsByOrderId(cart.OrderId);
                currentOrderDetails ??= new List<OrderDetailResponseModel>();

                // 3. Xử lý từng mục trong yêu cầu cập nhật
                foreach (var item in model.Items)
                {
                    var existingDetail = currentOrderDetails.FirstOrDefault(od => od.ProductId == item.ProductId);

                    if (existingDetail == null)
                    {
                        // Nếu sản phẩm không tồn tại trong giỏ hàng, bỏ qua hoặc có thể thêm mới (tùy yêu cầu)
                        continue;
                    }

                    if (item.Quantity <= 0)
                    {
                        // Nếu số lượng <= 0, xóa sản phẩm khỏi giỏ hàng
                        var deleteResult = await _orderDetailService.DeleteOrderDetailAsync(existingDetail.OrderDetailId);
                        if (!deleteResult.Success)
                        {
                            return new BaseResponse<OrderReponseModel>
                            {
                                Code = deleteResult.Code,
                                Success = false,
                                Message = deleteResult.Message,
                                Data = null
                            };
                        }
                        // Xóa khỏi danh sách hiện tại
                        currentOrderDetails.Remove(existingDetail);
                    }
                    else
                    {
                        // Cập nhật số lượng và giá
                        var updateModel = new UpdateOrderDetailRequestModel
                        {
                            OrderDetailId = existingDetail.OrderDetailId,
                            Quantity = item.Quantity,
                            Price = item.Price
                        };
                        var updateResult = await _orderDetailService.UpdateOrderDetailAsync(updateModel);
                        if (!updateResult.Success)
                        {
                            return new BaseResponse<OrderReponseModel>
                            {
                                Code = updateResult.Code,
                                Success = false,
                                Message = updateResult.Message,
                                Data = null
                            };
                        }
                        // Cập nhật lại thông tin trong currentOrderDetails
                        existingDetail.Quantity = item.Quantity;
                        existingDetail.UnitPrice = item.Price;
                    }
                }

                // 4. Tính lại totalAmount dựa trên tất cả OrderDetails
                decimal totalAmount = currentOrderDetails.Sum(od => od.Quantity * od.UnitPrice);
                cart.TotalAmount = totalAmount;
                await _orderRepository.UpdateOrderAsync(cart);

                // 5. Chuẩn bị phản hồi
                var response = _mapper.Map<OrderReponseModel>(cart);
                response.OrderDetails = currentOrderDetails;

                return new BaseResponse<OrderReponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Cập nhật giỏ hàng thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderReponseModel>> UpdateOrderAsync(UpdateOrderRequestModel model, int orderId)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Không tìm thấy đơn hàng để cập nhật!",
                        Data = null
                    };
                }

                // Cập nhật thông tin đơn hàng
                _mapper.Map(model, order);
                decimal totalAmount = 0;
                foreach (var product in model.Products)
                {
                    totalAmount += product.Price * product.Quantity;
                }
                order.TotalAmount = totalAmount;
                await _orderRepository.UpdateOrderAsync(order);

                // Xóa các OrderDetails cũ
                var currentOrderDetails = await _orderDetailService.GetListOrderDetailsByOrderId(orderId);
                // Giả sử có phương thức xóa trong repository, nếu không thì cần thêm
                // Hiện tại tôi sẽ bỏ qua phần xóa vì chưa có phương thức xóa trong IOrderDetailRepository

                // Thêm lại các OrderDetails mới
                var createOrderDetails = new CreateOrderDetailRequestModel
                {
                    OrderId = orderId,
                    Products = model.Products
                };
                var orderDetails = await _orderDetailService.CreateListOrderDetails(createOrderDetails);
                if (orderDetails == null || !orderDetails.Any())
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 500,
                        Success = false,
                        Message = "Không thể tạo chi tiết đơn hàng!",
                        Data = null
                    };
                }

                var response = _mapper.Map<OrderReponseModel>(order);
                response.OrderDetails = orderDetails;
                return new BaseResponse<OrderReponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Cập nhật đơn hàng thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<OrderReponseModel>> UpdateOrderStatusAsync(int orderId, string newStatus)
        {
            try
            {
                // 1. Kiểm tra đơn hàng tồn tại
                var order = await _orderRepository.GetOrderByIdAsync(orderId);
                if (order == null || order.IsDeleted)
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Đơn hàng không tồn tại hoặc đã bị xóa!",
                        Data = null
                    };
                }

                // 2. Danh sách trạng thái hợp lệ
                var validStatuses = new List<string> { "Cart", "Paid", "Confirmed", "Processing", "Completed", "Cancelled", "Rejected" };
                if (!validStatuses.Contains(newStatus))
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 400,
                        Success = false,
                        Message = $"Trạng thái '{newStatus}' không hợp lệ! Các trạng thái hợp lệ: {string.Join(", ", validStatuses)}",
                        Data = null
                    };
                }

                // 3. Kiểm tra chuyển đổi trạng thái hợp lệ
                bool isValidTransition = IsValidStatusTransition(order.Status, newStatus);
                if (!isValidTransition)
                {
                    return new BaseResponse<OrderReponseModel>
                    {
                        Code = 400,
                        Success = false,
                        Message = $"Không thể chuyển từ trạng thái '{order.Status}' sang '{newStatus}'!",
                        Data = null
                    };
                }

                // 4. Cập nhật trạng thái
                order.Status = newStatus;
                await _orderRepository.UpdateOrderAsync(order);

                // 5. Lấy danh sách OrderDetails để trả về response
                var orderDetails = await _orderDetailService.GetListOrderDetailsByOrderId(orderId);

                // 6. Chuẩn bị response
                var response = _mapper.Map<OrderReponseModel>(order);
                response.OrderDetails = orderDetails;

                return new BaseResponse<OrderReponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = $"Cập nhật trạng thái đơn hàng thành '{newStatus}' thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }
        private bool IsValidStatusTransition(string currentStatus, string newStatus)
        {
            // Trạng thái cuối: không thể chuyển đổi
            if (currentStatus == "Completed" || currentStatus == "Cancelled" || currentStatus == "Rejected")
            {
                return false;
            }

            // Kiểm tra chuyển đổi theo luồng
            switch (currentStatus)
            {
                case "Cart":
                    return newStatus == "Paid" || newStatus == "Cancelled" || newStatus == "Rejected";
                case "Paid":
                    return newStatus == "Confirmed" || newStatus == "Cancelled" || newStatus == "Rejected";
                case "Confirmed":
                    return newStatus == "Processing" || newStatus == "Cancelled" || newStatus == "Rejected";
                case "Processing":
                    return newStatus == "Completed" || newStatus == "Cancelled" || newStatus == "Rejected";
                default:
                    return false; // Không nên xảy ra vì đã kiểm tra validStatuses
            }
        }
    }
}
