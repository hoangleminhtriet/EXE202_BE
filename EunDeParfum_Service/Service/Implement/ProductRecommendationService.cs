using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductRecoment;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<BaseResponse<ProductRecommendationResponseModel>> CreateProductRecommendationAsync(CreateProductRecommendationRequestModel model)
        {
            try
            {
                var recommendation = _mapper.Map<ProductRecommendation>(model);
                await _productRecommendationRepository.CreateProductRecommendationAsync(recommendation);
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Product recommendation created successfully!",
                    Data = _mapper.Map<ProductRecommendationResponseModel>(recommendation)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductRecommendationResponseModel>> UpdateProductRecommendationAsync(UpdateProductRecommendationRequestModel model, int id)
        {
            try
            {
                var existingRecommendation = await _productRecommendationRepository.GetProductRecommendationByIdAsync(id);
                if (existingRecommendation == null)
                {
                    return new BaseResponse<ProductRecommendationResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product recommendation not found!",
                        Data = null
                    };
                }
                _mapper.Map(model, existingRecommendation);
                await _productRecommendationRepository.UpdateProductRecommendationAsync(existingRecommendation);
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Product recommendation updated successfully!",
                    Data = _mapper.Map<ProductRecommendationResponseModel>(existingRecommendation)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductRecommendationResponseModel>> DeleteProductRecommendationAsync(int id, bool status)
        {
            try
            {
                var result = await _productRecommendationRepository.DeleteProductRecommendationAsync(id);
                if (!result)
                {
                    return new BaseResponse<ProductRecommendationResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product recommendation not found!",
                        Data = null
                    };
                }
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Product recommendation deleted successfully!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductRecommendationResponseModel>> GetProductRecommendationByIdAsync(int id)
        {
            try
            {
                var recommendation = await _productRecommendationRepository.GetProductRecommendationByIdAsync(id);
                if (recommendation == null)
                {
                    return new BaseResponse<ProductRecommendationResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product recommendation not found!",
                        Data = null
                    };
                }
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = _mapper.Map<ProductRecommendationResponseModel>(recommendation)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductRecommendationResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ProductRecommendationResponseModel>> GetAllProductRecommendationsAsync(GetAllProductRecommendationRequestModel model)
        {
            try
            {
                var recommendations = await _productRecommendationRepository.GetAllProductRecommendationsAsync();
                var result = _mapper.Map<List<ProductRecommendationResponseModel>>(recommendations);

                return new DynamicResponse<ProductRecommendationResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = new MegaData<ProductRecommendationResponseModel>
                    {
                        PageData = result
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ProductRecommendationResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }
    }
}
