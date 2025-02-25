using EunDeParfum_Service.RequestModel.UserAnswer;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/UserAnswer")]
    [ApiController]
    public class UserAnswerController : ControllerBase
    {
        private readonly IUserAnswersService _userAnswersService;
        public UserAnswerController(IUserAnswersService userAnswersService)
        {
            _userAnswersService = userAnswersService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllUserAnswers(GetAllUserAnswerRequestModel model)
        {
            try
            {
                var result = await _userAnswersService.GetAllUserAnswers(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserAnswerById(int id)
        {
            try
            {
                var result = await _userAnswersService.GetUserAnswerByIdAsync(id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateUserAnswer(CreateUserAnswerRequestModel model)
        {
            try
            {
                var result = await _userAnswersService.CreateUserAnswerAsync(model);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateUserAnswer([FromBody] CreateUserAnswerRequestModel model, int id)
        {
            try
            {
                var result = await _userAnswersService.UpdateUserAnswerAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteUserAnswer(int id, bool status)
        {
            try
            {
                var result = await _userAnswersService.DeleteUserAnswerAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpDelete]
        public async Task<IActionResult> DeleteUserAnswerAsync(int id, bool status)
        {
            try
            {
                var result = await _userAnswersService.DeleteUserAnswerAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
