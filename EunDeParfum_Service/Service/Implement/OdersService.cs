using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Order;
using EunDeParfum_Service.ResponseModel.OrderDetail;
using EunDeParfum_Service.Service.Interface;
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

        public OdersService(IOrderRepository orderRepository, IMapper mapper, IOrderDetailService orderDetailService)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _orderDetailService = orderDetailService;
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

        public Task<BaseResponse<OrderReponseModel>> DeleteOrderAsync(int orderId, bool status)
        {
            throw new NotImplementedException();
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
                var orderIds = orders.Select(o => o.OrderId).ToList();
                var allOrderDetails = await _orderDetailService.GetListOrderDetailsByListOrderIds(orderIds);

                // 5️⃣ Chuyển OrderDetails thành Dictionary (Key: OrderId, Value: List<OrderDetailResponseModel>)
                var orderDetailsDict = allOrderDetails
                    .GroupBy(od => od.OrderId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // 6️⃣  Chuyển dữ liệu sang Dictionary<OrderId, OrderReponseModel>
                var responseList = pageOrders.Select(order => new OrderReponseModel
                {
                    OrderId = order.OrderId,
                    TotalAmount = order.TotalAmount,
                    OrderDate = order.OrderDate,
                    OrderDetails = orderDetailsDict.ContainsKey(order.OrderId) ? orderDetailsDict[order.OrderId] : new List<OrderDetailResponseModel>()
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
                        keyWord = null, // Bạn có thể thêm nếu cần tìm kiếm theo keyword
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

        public Task<BaseResponse<OrderReponseModel>> UpdateOrderAsync(UpdateOrderRequestModel model, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
