using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/product-recommendations")]
    [ApiController]
    public class ProductRecommendationController : ControllerBase
    {
        private readonly IProductRecommendationService _productRecommendationService;

        public ProductRecommendationController(IProductRecommendationService productRecommendationService)
        {
            _productRecommendationService = productRecommendationService;
        }

        // Tạo mới một ProductRecommendation (Admin, Manager)
        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateProductRecommendation([FromBody] CreateProductRecommendationRequestModel model)
        {
            try
            {
                var result = await _productRecommendationService.CreateProductRecommendationAsync(model);
                return CreatedAtAction(nameof(GetProductRecommendationById), new { id = result.Id }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error while creating product recommendation", error = ex.Message });
            }
        }

        // Lấy tất cả ProductRecommendations có lọc và phân trang
        [HttpGet]
        public async Task<IActionResult> GetAllProductRecommendations([FromQuery] int? productId, [FromQuery] bool? status, [FromQuery] int pageNum = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _productRecommendationService.GetAllProductRecommendationsAsync(productId, status, pageNum, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error while fetching product recommendations", error = ex.Message });
            }
        }

        // Lấy ProductRecommendation theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductRecommendationById(int id)
        {
            try
            {
                var result = await _productRecommendationService.GetProductRecommendationByIdAsync(id);
                if (result == null)
                {
                    return NotFound(new { message = "Product recommendation not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error while fetching product recommendation", error = ex.Message });
            }
        }

        // Cập nhật ProductRecommendation (Admin, Manager)
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProductRecommendation([FromBody] CreateProductRecommendationRequestModel model, int id)
        {
            try
            {
                await _productRecommendationService.UpdateProductRecommendationAsync(model, id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error while updating product recommendation", error = ex.Message });
            }
        }

        // Xóa ProductRecommendation (thay đổi trạng thái, không xóa vật lý) (Admin, Manager)
        [Authorize(Roles = "Admin, Manager")]
        [HttpPatch("{id}/change-status")]
        public async Task<IActionResult> ChangeProductRecommendationStatus(int id)
        {
            try
            {
                await _productRecommendationService.DeleteProductRecommendationAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error while changing product recommendation status", error = ex.Message });
            }
        }
    }
}
