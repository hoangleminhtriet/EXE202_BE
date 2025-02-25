using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.ProductCate;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductCate;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Implement
{
    public class ProductCategoriesService : IProductCategoriesService
    {
        private readonly IProductCateRepository _productCateRepository;
        private readonly IMapper _mapper;

        public ProductCategoriesService(IProductCateRepository productCateRepository, IMapper mapper)
        {
            _productCateRepository = productCateRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductCateResponseModel>> CreateProductCateAsync(CreateProductCateRequestModel model)
        {
            try
            {
                var productCate = _mapper.Map<ProductCategory>(model);
                await _productCateRepository.CreateProductCateAsync(productCate);
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Product category created successfully!",
                    Data = _mapper.Map<ProductCateResponseModel>(productCate)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductCateResponseModel>> UpdateProductCateAsync(CreateProductCateRequestModel model, int id)
        {
            try
            {
                var existingProductCate = await _productCateRepository.GetProducCateByIdAsync(id);
                if (existingProductCate == null)
                {
                    return new BaseResponse<ProductCateResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product category not found!",
                        Data = null
                    };
                }
                _mapper.Map(model, existingProductCate);
                await _productCateRepository.UpdateProductCateAsync(existingProductCate);
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Product category updated successfully!",
                    Data = _mapper.Map<ProductCateResponseModel>(existingProductCate)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductCateResponseModel>> DeleteProductCateAsync(int productCateId, bool status)
        {
            try
            {
                var result = await _productCateRepository.DeleteProductCateAsync(productCateId);
                if (!result)
                {
                    return new BaseResponse<ProductCateResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product category not found!",
                        Data = null
                    };
                }
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Product category deleted successfully!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductCateResponseModel>> GetProductCateById(int productCateId)
        {
            try
            {
                var productCate = await _productCateRepository.GetProducCateByIdAsync(productCateId);
                if (productCate == null)
                {
                    return new BaseResponse<ProductCateResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Product category not found!",
                        Data = null
                    };
                }
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = _mapper.Map<ProductCateResponseModel>(productCate)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCateResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ProductCateResponseModel>> GetAllProductCate(GetAllProductCateRequestModel model)
        {
            try
            {
                var productCates = await _productCateRepository.GetAllProductCateAsync();
                var result = _mapper.Map<List<ProductCateResponseModel>>(productCates);
                return new DynamicResponse<ProductCateResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = new MegaData<ProductCateResponseModel>
                    {
                        PageData = result
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ProductCateResponseModel>
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
