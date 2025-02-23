using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsService _productsService;
        public ProductController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpPost("GetAllProduct")]
        public async Task<IActionResult> GetAllProduct(GetAllProductRequestModel model)
        {
            try
            {
                var result = await _productsService.GetAllProduct(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetProductbyId")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var result = await _productsService.GetProductById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromForm]CreateProductRequestModel model)
        {
            try
            {
                var result = await _productsService.CreateProductAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]                                                                                                                   
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateProduct([FromBody] CreateProductRequestModel model, int id)
        {
            try
            {
                var result = await _productsService.UpdateProductAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteProduct(int id, bool status)
        {
            try
            {
                var result = await _productsService.DeleteProductAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductAsync(int id, bool status)
        {
            try
            {
                var result = await _productsService.DeleteProductAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
