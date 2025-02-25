using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        [HttpPost("GetAllProductRecommendations")]
        public async Task<IActionResult> GetAllProductRecommendations(GetAllProductRecommendationRequestModel model)
        {
            try
            {
                var result = await _productRecommendationService.GetAllProductRecommendationsAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductRecommendationById/{id}")]
        public async Task<IActionResult> GetProductRecommendationById(int id)
        {
            try
            {
                var result = await _productRecommendationService.GetProductRecommendationByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("CreateProductRecommendation")]
        public async Task<IActionResult> CreateProductRecommendation([FromForm] CreateProductRecommendationRequestModel model)
        {
            try
            {
                var result = await _productRecommendationService.CreateProductRecommendationAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut("UpdateProductRecommendation/{id}")]
        public async Task<IActionResult> UpdateProductRecommendation([FromBody] UpdateProductRecommendationRequestModel model, int id)
        {
            try
            {
                var result = await _productRecommendationService.UpdateProductRecommendationAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("DeleteProductRecommendation/{id}")]
        public async Task<IActionResult> DeleteProductRecommendation(int id, bool status)
        {
            try
            {
                var result = await _productRecommendationService.DeleteProductRecommendationAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
