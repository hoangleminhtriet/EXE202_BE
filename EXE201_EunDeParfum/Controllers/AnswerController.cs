using EunDeParfum_Service.RequestModel.Answer;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswersService _answersService;
        public AnswerController(IAnswersService answersService)
        {
            _answersService = answersService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllAnswer(GetAllAnswerRequestModel model)
        {
            try
            {
                var result = await _answersService.GetAllAnswers(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            try
            {
                var result = await _answersService.GetAnswerByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(CreateAnswerRequestModel model)
        {
            try
            {
                var result = await _answersService.CreateAnswerAsync(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateAnswer([FromBody] CreateAnswerRequestModel model, int id)
        {
            try
            {
                var result = await _answersService.UpdateAnswerAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteAnswer(int id, bool status)
        {
            try
            {
                var result = await _answersService.DeleteAnswerAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswerAsync(int id, bool status)
        {
            try
            {
                var result = await _answersService.DeleteAnswerAsync(id,status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
