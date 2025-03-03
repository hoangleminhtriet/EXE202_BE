using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Order;
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
                    Data = _mapper.Map<OrderReponseModel>(createOrderDetails)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderReponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public Task<BaseResponse<OrderReponseModel>> DeleteOrderAsync(int orderId, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<DynamicResponse<OrderReponseModel>> GetAllOrdersAsync(GetAllOrderRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<OrderReponseModel>> GetOrderByIdAsync(int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<OrderReponseModel>> UpdateOrderAsync(UpdateOrderRequestModel model, int orderId)
        {
            throw new NotImplementedException();
        }
    }
}
