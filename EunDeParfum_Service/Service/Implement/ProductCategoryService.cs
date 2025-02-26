using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.ProductCategory;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.ProductCategory;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IMapper _mapper;

        public ProductCategoryService(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            _productCategoryRepository = productCategoryRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductCategoryResponseModel>> CreateProductCategoryAsync(CreateProductCategoryRequestModel model)
        {
            try
            {
                var productCategory = _mapper.Map<ProductCategory>(model);
                await _productCategoryRepository.CreateProductCategoryAsync(productCategory);
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create ProductCategory success!",
                    Data = _mapper.Map<ProductCategoryResponseModel>(productCategory)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductCategoryResponseModel>> UpdateProductCategoryAsync(UpdateProductCategoryRequestModel model)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetProductCategoryAsync(model.ProductId, model.CategoryId);
                if (productCategory == null)
                {
                    return new BaseResponse<ProductCategoryResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "ProductCategory not found!",
                        Data = null
                    };
                }
                _mapper.Map(model, productCategory);
                await _productCategoryRepository.UpdateProductCategoryAsync(productCategory);
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update success!",
                    Data = _mapper.Map<ProductCategoryResponseModel>(productCategory)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteProductCategoryAsync(DeleteProductCategoryRequestModel model)
        {
            try
            {
                var success = await _productCategoryRepository.DeleteProductCategoryAsync(model.ProductId, model.CategoryId);
                return new BaseResponse<bool>()
                {
                    Code = success ? 200 : 404,
                    Success = success,
                    Message = success ? "Delete success!" : "ProductCategory not found!",
                    Data = success
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = false
                };
            }
        }

        public async Task<BaseResponse<ProductCategoryResponseModel>> GetProductCategoryAsync(int productId, int categoryId)
        {
            try
            {
                var productCategory = await _productCategoryRepository.GetProductCategoryAsync(productId, categoryId);
                if (productCategory == null)
                {
                    return new BaseResponse<ProductCategoryResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "ProductCategory not found!",
                        Data = null
                    };
                }
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ProductCategoryResponseModel>(productCategory)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductCategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ProductCategoryResponseModel>> GetCategoriesByProductIdAsync(int productId)
        {
            try
            {
                var list = await _productCategoryRepository.GetCategoriesByProductIdAsync(productId);
                var mappedList = _mapper.Map<List<ProductCategoryResponseModel>>(list);
                return new DynamicResponse<ProductCategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<ProductCategoryResponseModel>()
                    {
                        PageData = mappedList
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ProductCategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ProductCategoryResponseModel>> GetProductsByCategoryIdAsync(int categoryId)
        {
            try
            {
                var list = await _productCategoryRepository.GetProductsByCategoryIdAsync(categoryId);
                var mappedList = _mapper.Map<List<ProductCategoryResponseModel>>(list);
                return new DynamicResponse<ProductCategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<ProductCategoryResponseModel>()
                    {
                        PageData = mappedList
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ProductCategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }
}
