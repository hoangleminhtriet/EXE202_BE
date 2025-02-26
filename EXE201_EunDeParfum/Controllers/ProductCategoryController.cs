using EunDeParfum_Service.RequestModel.ProductCategory;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/ProductCategory")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private readonly IProductCategoryService _productCategoryService;

        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }

        [HttpPost("SearchByProductId/{productId}")]
        public async Task<IActionResult> GetCategoriesByProductId(int productId)
        {
            try
            {
                var result = await _productCategoryService.GetCategoriesByProductIdAsync(productId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("SearchByCategoryId/{categoryId}")]
        public async Task<IActionResult> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var result = await _productCategoryService.GetProductsByCategoryIdAsync(categoryId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductCategory/{productId}/{categoryId}")]
        public async Task<IActionResult> GetProductCategory(int productId, int categoryId)
        {
            try
            {
                var result = await _productCategoryService.GetProductCategoryAsync(productId, categoryId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Create")]
        public async Task<IActionResult> CreateProductCategory([FromBody] CreateProductCategoryRequestModel model)
        {
            try
            {
                var result = await _productCategoryService.CreateProductCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProductCategory([FromBody] UpdateProductCategoryRequestModel model)
        {
            try
            {
                var result = await _productCategoryService.UpdateProductCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteProductCategory([FromBody] DeleteProductCategoryRequestModel model)
        {
            try
            {
                var result = await _productCategoryService.DeleteProductCategoryAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
