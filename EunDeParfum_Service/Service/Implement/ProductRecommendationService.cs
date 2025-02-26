using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductRecommendation;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class ProductRecommendationService : IProductRecommendationService
    {
        private readonly IProductRecommendationRepository _productRecommendationRepository;
        private readonly IMapper _mapper;

        public ProductRecommendationService(IProductRecommendationRepository productRecommendationRepository, IMapper mapper)
        {
            _productRecommendationRepository = productRecommendationRepository;
            _mapper = mapper;
        }

        // Tạo mới một ProductRecommendation
        public async Task<ProductRecommendationResponseModel> CreateProductRecommendationAsync(CreateProductRecommendationRequestModel model)
        {
            try
            {
                var productRecommendation = _mapper.Map<ProductRecommendation>(model);
                productRecommendation.Status = true; // Trạng thái mặc định là true

                await _productRecommendationRepository.CreateProductRecommendationAsync(productRecommendation);

                return _mapper.Map<ProductRecommendationResponseModel>(productRecommendation);
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error while creating ProductRecommendation", ex);
            }
        }

        // Lấy tất cả các ProductRecommendation với bộ lọc và phân trang
        public async Task<List<ProductRecommendationResponseModel>> GetAllProductRecommendationsAsync(int? productId, bool? status, int pageNum, int pageSize)
        {
            try
            {
                // Lấy tất cả ProductRecommendation từ repository
                var productRecommendations = await _productRecommendationRepository.GetAllProductRecommendationsAsync(productId, status, pageNum, pageSize);

                // Chuyển đổi sang danh sách response model
                var mappedRecommendations = _mapper.Map<List<ProductRecommendationResponseModel>>(productRecommendations);

                // Trả về danh sách đã được chuyển đổi
                return mappedRecommendations;
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error while fetching ProductRecommendations", ex);
            }
        }

        // Lấy thông tin ProductRecommendation theo ID
        public async Task<ProductRecommendationResponseModel> GetProductRecommendationByIdAsync(int id)
        {
            try
            {
                var productRecommendation = await _productRecommendationRepository.GetProductRecommendationByIdAsync(id);

                if (productRecommendation == null)
                {
                    throw new Exception("ProductRecommendation not found");
                }

                return _mapper.Map<ProductRecommendationResponseModel>(productRecommendation);
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error while fetching ProductRecommendation by ID", ex);
            }
        }

        // Cập nhật ProductRecommendation
        public async Task UpdateProductRecommendationAsync(CreateProductRecommendationRequestModel model, int id)
        {
            try
            {
                var existingProductRecommendation = await _productRecommendationRepository.GetProductRecommendationByIdAsync(id);
                if (existingProductRecommendation == null)
                {
                    throw new Exception("ProductRecommendation not found");
                }

                _mapper.Map(model, existingProductRecommendation);
                await _productRecommendationRepository.UpdateProductRecommendationAsync(existingProductRecommendation);
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error while updating ProductRecommendation", ex);
            }
        }

        // Xóa ProductRecommendation
        public async Task DeleteProductRecommendationAsync(int id)
        {
            try
            {
                var existingProductRecommendation = await _productRecommendationRepository.GetProductRecommendationByIdAsync(id);
                if (existingProductRecommendation == null)
                {
                    throw new Exception("ProductRecommendation not found");
                }

                existingProductRecommendation.Status = false; // Chỉ thay đổi trạng thái thay vì xóa hoàn toàn
                await _productRecommendationRepository.UpdateProductRecommendationAsync(existingProductRecommendation);
            }
            catch (Exception ex)
            {
                throw new Exception("Server Error while deleting ProductRecommendation", ex);
            }
        }
    }
}
