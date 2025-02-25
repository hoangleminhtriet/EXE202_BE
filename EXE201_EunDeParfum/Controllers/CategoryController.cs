using EunDeParfum_Service.RequestModel.Category;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoriesService _categoriesService;

        public CategoryController(ICategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpPost("GetAllCategories")]
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

        [HttpGet("GetCategoryById/{id}")]
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

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryRequestModel model)
        {
            try
            {
                var result = await _categoriesService.CreateCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("UpdateCategory/{id}")]
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
    }
}
