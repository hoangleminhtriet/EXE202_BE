using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Category;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Implement
{
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoriesService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<CategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                await _categoryRepository.CreateCategoryAsync(category);
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 201,
                    Success = true,
                    Message = "Category created successfully!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CategoryResponseModel>> UpdateCategoryAsync(CreateCategoryRequestModel model, int id)
        {
            try
            {
                var existingCategory = await _categoryRepository.GetCategoryByIdAsync(id);
                if (existingCategory == null)
                {
                    return new BaseResponse<CategoryResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }
                _mapper.Map(model, existingCategory);
                await _categoryRepository.UpdateCategoryAsync(existingCategory);
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Category updated successfully!",
                    Data = _mapper.Map<CategoryResponseModel>(existingCategory)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CategoryResponseModel>> DeleteCategoryAsync(int categoryId, bool status)
        {
            try
            {
                var result = await _categoryRepository.DeleteCategoryAsync(categoryId);
                if (!result)
                {
                    return new BaseResponse<CategoryResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Category deleted successfully!",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<CategoryResponseModel>> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return new BaseResponse<CategoryResponseModel>
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model)
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                var result = _mapper.Map<List<CategoryResponseModel>>(categories);
                return new DynamicResponse<CategoryResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = "Success!",
                    Data = new MegaData<CategoryResponseModel>
                    {
                        PageData = result
                    }
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CategoryResponseModel>
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