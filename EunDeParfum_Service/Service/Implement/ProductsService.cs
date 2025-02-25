using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Product;
using EunDeParfum_Service.ResponseModel.Review;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class ProductsService : IProductsService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductsService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ProductResponseModel>> CreateProductAsync(CreateProductRequestModel model)
        {
            try
            {
                var product = _mapper.Map<Product>(model);
                product.CreatedAt = DateTime.UtcNow;
                product.UpdatedAt = DateTime.UtcNow;
                await _productRepository.CreateProductAsync(product);
                

                
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Product success!.",
                    Data = _mapper.Map<ProductResponseModel>(product)
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = ex.Message,
                    Data = null
                };
            }

        }

        public async Task<BaseResponse<ProductResponseModel>> DeleteProductAsync(int reviewId, bool status)
        {
            throw new NotImplementedException();
        }

        public async Task<DynamicResponse<ProductResponseModel>> GetAllProduct(GetAllProductRequestModel model)
        {
            try
            {
                var listProduct = _productRepository.GetAllProductAsync();
                var result = _mapper.Map<List<ProductResponseModel>>(listProduct);
                var pageProduct = result
                    .OrderBy(p => p.ProductId)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<ProductResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<ProductResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageProduct.PageNumber,
                            Size = pageProduct.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageProduct.PageCount,
                            TotalItem = pageProduct.TotalItemCount,
                        },
                        PageData = pageProduct.ToList()
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ProductResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductResponseModel>> GetProductById(int productId)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return new BaseResponse<ProductResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Review!.",
                        Data = null
                    };
                }
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ProductResponseModel>(product)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ProductResponseModel>> UpdateProductAsync(CreateProductRequestModel model, int id)
        {
            try
            {
                var product = await _productRepository.GetProductByIdAsync(id);
                product.UpdatedAt = DateTime.UtcNow;
                if (product == null)
                {
                    return new BaseResponse<ProductResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Review!.",
                        Data = null
                    };
                }
                await _productRepository.UpdateProductAsync(_mapper.Map(model, product));
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Review Success!.",
                    Data = _mapper.Map<ProductResponseModel>(product)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ProductResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }





        //public async Task<BaseResponse<ProductResponseModel>> CreateProductAsync(CreateProductRequestModel model)
        //{
        //    try
        //    {
        //        var product = _mapper.Map<Product>(model);

        //        await _productRepository.CreateProductAsync(product);
        //        return new BaseResponse<ProductResponseModel>()
        //        {
        //            Code = 201,
        //            Success = true,
        //            Message = "Create Product success!.",
        //            Data = _mapper.Map<ProductResponseModel>(product)
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<ProductResponseModel>()
        //        {
        //            Code = 500,
        //            Success = false,
        //            Message = "Server Error!",
        //            Data = null
        //        };
        //    }
        //}


    }
}
