using AutoMapper;
using EunDeParfum_Repository.Models;
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
                    Message = "Server Error!",
                    Data = null
                };
            }

        }

        public Task<BaseResponse<ProductResponseModel>> DeleteProductAsync(int reviewId, bool status)
        {
            throw new NotImplementedException();
        }

        public Task<DynamicResponse<ProductResponseModel>> GetAllProduct(GetAllReviewRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ProductResponseModel>> GetProductById(int reviewId)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<ProductResponseModel>> UpdateProductAsync(CreateProductRequestModel model, int id)
        {
            throw new NotImplementedException();
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
