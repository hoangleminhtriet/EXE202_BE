﻿using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.ResponseModel.BaseResponse;
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
        private readonly IProductRepository _productRepository; 
        private readonly IMapper _mapper;

        public OrderDetailService(
            IOrderDetailRepository orderDetailRepository,
            IProductRepository productRepository,
            IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
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

        public async Task<BaseResponse<bool>> DeleteOrderDetailAsync(int orderDetailId)
        {
            try
            {
                var success = await _orderDetailRepository.DeleteOrderDetailAsync(orderDetailId);
                if (!success)
                {
                    return new BaseResponse<bool>
                    {
                        Code = 500,
                        Success = false,
                        Message = "Không thể xóa chi tiết đơn hàng!",
                        Data = false
                    };
                }

                return new BaseResponse<bool>
                {
                    Code = 200,
                    Success = true,
                    Message = "Xóa chi tiết đơn hàng thành công!",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<List<OrderDetailResponseModel>> GetListOrderDetailsByListOrderIds(List<int> orderIds)
        {
            if (orderIds == null || !orderIds.Any()) return new List<OrderDetailResponseModel>();

            var orderDetails = await _orderDetailRepository.GetAll();
            var filteredOrderDetails = orderDetails
                .Where(od => orderIds.Contains(od.OrderId))
                .ToList();

            if (!filteredOrderDetails.Any()) return new List<OrderDetailResponseModel>();

            // Lấy danh sách productId từ orderDetails
            var productIds = filteredOrderDetails
                .Select(od => od.ProductId)
                .Distinct()
                .ToList();

            // Lấy thông tin sản phẩm
            var products = await _productRepository.GetProductsByIdsAsync(productIds);
            var productDict = products.ToDictionary(p => p.ProductId, p => p.Name);

            // Ánh xạ sang OrderDetailResponseModel
            var response = _mapper.Map<List<OrderDetailResponseModel>>(filteredOrderDetails);
            foreach (var detail in response)
            {
                detail.ProductName = productDict.ContainsKey(detail.ProductId)
                    ? productDict[detail.ProductId]
                    : "Không xác định";
            }

            return response;
        }

        public async Task<List<OrderDetailResponseModel>> GetListOrderDetailsByOrderId(int orderId)
        {
            var orderDetails = await _orderDetailRepository.GetListOrderDetailAsyncByOrderId(orderId);
            if (orderDetails == null || !orderDetails.Any()) return new List<OrderDetailResponseModel>();

            // Lấy danh sách productId
            var productIds = orderDetails
                .Select(od => od.ProductId)
                .Distinct()
                .ToList();

            // Lấy thông tin sản phẩm
            var products = await _productRepository.GetProductsByIdsAsync(productIds);
            var productDict = products.ToDictionary(p => p.ProductId, p => p.Name);

            // Ánh xạ sang OrderDetailResponseModel
            var response = _mapper.Map<List<OrderDetailResponseModel>>(orderDetails);
            foreach (var detail in response)
            {
                detail.ProductName = productDict.ContainsKey(detail.ProductId)
                    ? productDict[detail.ProductId]
                    : "Không xác định";
            }

            return response;
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> GetOrderDetailByIdAsync(int orderDetailId)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(orderDetailId);
                if (orderDetail == null)
                {
                    return new BaseResponse<OrderDetailResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Không tìm thấy chi tiết đơn hàng!",
                        Data = null
                    };
                }

                var response = _mapper.Map<OrderDetailResponseModel>(orderDetail);

                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Lấy chi tiết đơn hàng thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<bool>> RemoveOrderDetailsAsync(int orderId, List<int> orderDetailIds)
        {
            try
            {
                if (orderDetailIds == null || !orderDetailIds.Any())
                {
                    return new BaseResponse<bool>
                    {
                        Code = 400,
                        Success = false,
                        Message = "Danh sách OrderDetailIds không hợp lệ.",
                        Data = false
                    };
                }

                foreach (var orderDetailId in orderDetailIds)
                {
                    var result = await DeleteOrderDetailAsync(orderDetailId);
                    if (!result.Success)
                    {
                        return new BaseResponse<bool>
                        {
                            Code = result.Code,
                            Success = false,
                            Message = $"Không thể xóa chi tiết đơn hàng {orderDetailId}: {result.Message}",
                            Data = false
                        };
                    }
                }

                return new BaseResponse<bool>
                {
                    Code = 200,
                    Success = true,
                    Message = "Xóa các chi tiết đơn hàng thành công.",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi khi xóa chi tiết đơn hàng: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<OrderDetailResponseModel>> UpdateOrderDetailAsync(UpdateOrderDetailRequestModel model)
        {
            try
            {
                var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(model.OrderDetailId);
                if (orderDetail == null)
                {
                    return new BaseResponse<OrderDetailResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Không tìm thấy chi tiết đơn hàng để cập nhật!",
                        Data = null
                    };
                }

                // Cập nhật thông tin
                orderDetail.Quantity = model.Quantity;
                orderDetail.UnitPrice = model.Price;

                await _orderDetailRepository.UpdateOrderDetailAsync(orderDetail);

                var response = _mapper.Map<OrderDetailResponseModel>(orderDetail);

                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Cập nhật chi tiết đơn hàng thành công!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<OrderDetailResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }
    }
}
