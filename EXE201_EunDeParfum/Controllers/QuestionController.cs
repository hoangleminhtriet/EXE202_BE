using EunDeParfum_Service.RequestModel.Question;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllQuestions(GetAllQuestionRequestModel model)
        {
            try
            {
                var result = await _questionService.GetAllQuestions(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestionById(int id)
        {
            try
            {
                var result = await _questionService.GetQuestionById(id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateQuestion(CreateQuestionRequestModel model)
        {
            try
            {
                var result = await _questionService.CreateQuestionAsync(model);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateQuestion([FromBody] CreateQuestionRequestModel model, int id)
        {
            try
            {
                var result = await _questionService.UpdateQuestionAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles ="Admin, Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteQuestion(int id, bool status)
        {
            try
            {
                var result = await _questionService.DeleteQuestionAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestionAsync(int id, bool status)
        {
            try
            {
                var result = await _questionService.DeleteQuestionAsync(id,status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
