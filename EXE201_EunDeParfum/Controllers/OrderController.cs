using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.RequestModel.OrderDetail;
using EunDeParfum_Service.Service.Implement;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe.Climate;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOderService _oderService;
        private readonly IOrderDetailService _orderDetailService;

        public OrderController(IOderService oderService)
        {
            _oderService = oderService;
        }
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateOrderRequestModel model)
        {
            try
            {
                var result = await _oderService.CreateOrderAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(AddToCartRequestModel model)
        {
            try
            {
                var result = await _oderService.AddToCartAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Success = false, Message = $"Lỗi: {ex.Message}" });
            }
        }
        [HttpPut("update-cart")]
        public async Task<IActionResult> UpdateCart([FromBody] UpdateCartRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _oderService.UpdateCartAsync(model);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] UpdateOrderRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _oderService.UpdateOrderAsync(model, orderId);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpPut("update-status/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, [FromBody] string newStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _oderService.UpdateOrderStatusAsync(orderId, newStatus);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId, [FromQuery] bool status = true)
        {
            var result = await _oderService.DeleteOrderAsync(orderId, status);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAll(GetAllOrderRequestModel model)
        {
            try
            {
                var result = await _oderService.GetAllOrdersAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            try
            {
                var result = await _oderService.GetOrderByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("order-details")]
        public async Task<IActionResult> UpdateOrderDetail([FromBody] UpdateOrderDetailRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _orderDetailService.UpdateOrderDetailAsync(model);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpGet("order-details/{orderDetailId}")]
        public async Task<IActionResult> GetOrderDetailById(int orderDetailId)
        {
            var result = await _orderDetailService.GetOrderDetailByIdAsync(orderDetailId);
            if (!result.Success)
            {
                return StatusCode(result.Code, result);
            }

            return StatusCode(result.Code, result);
        }
        [HttpGet("GeneratePaymentLink/{orderId}")]
        public async Task<IActionResult> GeneratePaymentLink(int orderId)
        {
            try
            {
                var paymentLink = await _oderService.GeneratePaymentLinkForOrderAsync(orderId);
                return Ok(new
                {
                    Success = true,
                    Message = "Generate payment link successfully.",
                    PaymentUrl = paymentLink
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Success = false,
                    Message = $"Server error: {ex.Message}"
                });
            }
        }
    }
}
