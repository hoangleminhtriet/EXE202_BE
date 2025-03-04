using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOderService _oderService;

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
    }
}
