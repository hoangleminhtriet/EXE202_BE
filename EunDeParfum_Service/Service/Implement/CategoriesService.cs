using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel;
using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

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

        // Tạo mới một Category
        public async Task<BaseResponse<CategoryResponseModel>> CreateCategoryAsync(CreateCategoryRequestModel model)
        {
            try
            {
                var category = _mapper.Map<Category>(model);
                category.Status = true;  // Trạng thái mặc định là active
                await _categoryRepository.CreateCategoryAsync(category);
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Category success!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        // Cập nhật thông tin của Category
        public async Task<BaseResponse<CategoryResponseModel>> UpdateCategoryAsync(CreateCategoryRequestModel model, int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(id);
                if (category == null)
                {
                    return new BaseResponse<CategoryResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }

                // Cập nhật thông tin Category
                await _categoryRepository.UpdateCategoryAsync(_mapper.Map(model, category));
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Category success!",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        // Xóa hoặc thay đổi trạng thái Category
        public async Task<BaseResponse<CategoryResponseModel>> DeleteCategoryAsync(int categoryId, bool status)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return new BaseResponse<CategoryResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }

                category.Status = status;  // Thay đổi trạng thái của category (active/inactive)
                await _categoryRepository.UpdateCategoryAsync(category);

                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Category status updated successfully",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        // Lấy thông tin Category theo Id
        public async Task<BaseResponse<CategoryResponseModel>> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                if (category == null)
                {
                    return new BaseResponse<CategoryResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Category not found!",
                        Data = null
                    };
                }

                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Category found",
                    Data = _mapper.Map<CategoryResponseModel>(category)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CategoryResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        // Lấy tất cả Category với phân trang và tìm kiếm
        public async Task<DynamicResponse<CategoryResponseModel>> GetAllCategoriesAsync(GetAllCategoryRequestModel model)
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategoriesAsync();

                // Lọc theo keyword tìm kiếm nếu có
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    categories = categories
                        .Where(c => c.Name.ToLower().Contains(model.keyWord.ToLower()) || c.Description.ToLower().Contains(model.keyWord.ToLower()))
                        .ToList();
                }

                // Lọc theo trạng thái nếu có
                if (model.Status != null)
                {
                    categories = categories.Where(c => c.Status == model.Status).ToList();
                }

                var result = _mapper.Map<List<CategoryResponseModel>>(categories);

                // Phân trang
                var pageCategories = result
                    .OrderBy(c => c.CategoryId)
                    .ToPagedList(model.pageNum, model.pageSize);

                return new DynamicResponse<CategoryResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = new MegaData<CategoryResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageCategories.PageNumber,
                            Size = pageCategories.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageCategories.PageCount,
                            TotalItem = pageCategories.TotalItemCount
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageCategories.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CategoryResponseModel>()
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
