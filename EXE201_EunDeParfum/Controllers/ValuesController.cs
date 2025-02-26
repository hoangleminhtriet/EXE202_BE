using EunDeParfum_Service.RequestModel;
using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;
        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // API: Lấy danh sách tất cả các Category
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllCategories(GetAllCategoryRequestModel model)
        {
            try
            {
                var result = await _categoriesService.GetAllCategoriesAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // API: Lấy thông tin Category theo Id
        [HttpGet("Getby/{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            try
            {
                var result = await _categoriesService.GetCategoryByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // API: Tạo mới Category
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateCategory(CreateCategoryRequestModel model)
        {
            try
            {
                var result = await _categoriesService.CreateCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // API: Cập nhật thông tin Category
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateCategory([FromBody] CreateCategoryRequestModel model, int id)
        {
            try
            {
                var result = await _categoriesService.UpdateCategoryAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // API: Thay đổi trạng thái của Category
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> ChangeCategoryStatus(int id, bool status)
        {
            try
            {
                var result = await _categoriesService.DeleteCategoryAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // API: Xóa Category
        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteCategory(int id, bool status)
        {
            try
            {
                var result = await _categoriesService.DeleteCategoryAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
