using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.ResponseModel.OrderDetail;
using EunDeParfum_Service.Service.Interface;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Implement
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }
        public async Task<List<OrderDetailResponseModel>> CreateListOrderDetails(CreateOrderDetailRequestModel model)
        {
            var orderDetails = model.Products.Select(od => new OrderDetail
            {
                OrderId = model.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.Price
            }).ToList();

            await _orderDetailRepository.CreateListOrderDetailAsync(orderDetails);

            var response = orderDetails.Select(od => new OrderDetailResponseModel
            {
                OrderDetailId = od.OrderDetailId,
                OrderId = od.OrderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice
            }).ToList();
            return response;
        }

        public async Task<List<OrderDetailResponseModel>> GetListOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailRepository.GetListOrderDetailAsyncByOrderId(orderId);
            return _mapper.Map<List<OrderDetailResponseModel>>(orderDetails);
        }

        public Task<List<OrderDetailResponseModel>> GetOrderDetailByIdAsync(int orderDetailId)
        {
            throw new NotImplementedException();
        }
    }
}
