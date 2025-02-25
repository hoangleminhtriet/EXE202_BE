using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EunDeParfum_Service.RequestModel.ProductCate;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Threading.Tasks;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCateController : ControllerBase
    {
        private readonly IProductCategoriesService _productCategoriesService;

        public ProductCateController(IProductCategoriesService productCategoriesService)
        {
            _productCategoriesService = productCategoriesService;
        }

        [HttpGet("GetAllProductCate")]
        public async Task<IActionResult> GetAllProductCate([FromQuery] GetAllProductCateRequestModel model)
        {
            try
            {
                var result = await _productCategoriesService.GetAllProductCate(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Server error", Error = ex.Message });
            }
        }

        [HttpGet("GetProductCateById/{id}")]
        public async Task<IActionResult> GetProductCateById(int id)
        {
            try
            {
                var result = await _productCategoriesService.GetProductCateById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Server error", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("CreateProductCate")]
        public async Task<IActionResult> CreateProductCate([FromBody] CreateProductCateRequestModel model)
        {
            try
            {
                var result = await _productCategoriesService.CreateProductCateAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Server error", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateProductCate([FromBody] CreateProductCateRequestModel model, int id)
        {
            try
            {
                var result = await _productCategoriesService.UpdateProductCateAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Server error", Error = ex.Message });
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteProductCate(int id, bool status)
        {
            try
            {
                var result = await _productCategoriesService.DeleteProductCateAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = "Server error", Error = ex.Message });
            }
        }
    }
}
