using EunDeParfum_Service.RequestModel.Payment;
using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> CreatePayment([FromBody] CreatePaymentRequestModel model)
        {
            try
            {
                var result = await _paymentService.CreatePaymentAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating payment", ex);
            }
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdatePayment([FromBody] CreatePaymentRequestModel model, int id)
        {
            try
            {
                var result = await _paymentService.UpdatePaymentAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating payment", ex);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            try
            {
                var result = await _paymentService.GetPaymentByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving payment", ex);
            }
        }

        [HttpPost("GetAll")]
        public async Task<IActionResult> GetAllPayments([FromBody] GetAllPaymentRequestModel model)
        {
            try
            {
                var result = await _paymentService.GetAllPaymentsAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all payments", ex);
            }
        }

        [HttpGet("GetPaymentsByStatus")]
        public async Task<IActionResult> GetPaymentsByStatus([FromQuery] string status)
        {
            try
            {
                var result = await _paymentService.GetPaymentsByStatusAsync(status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
