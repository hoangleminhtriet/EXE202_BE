using EunDeParfum_Service.RequestModel.Result;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Result")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;
        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllResults(GetAllResultRequestModel model)
        {
            try
            {
                var result = await _resultService.GetAllResults(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetResultById(int id)
        {
            try
            {
                var result = await _resultService.GetResultByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateResult(CreateResultRequestModel model)
        {
            try
            {
                var result = await _resultService.CreateResultAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error: ", ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateResult([FromBody] CreateResultRequestModel model, int id)
        {
            try
            {
                var result = await _resultService.UpdateResultAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteResult(int id, bool status)
        {
            try
            {
                var result = await _resultService.DeleteResultAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResultAsync(int id, bool status)
        {
            try
            {
                var result = await _resultService.DeleteResultAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
