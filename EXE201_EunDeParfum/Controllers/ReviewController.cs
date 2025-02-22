using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Review")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost("Search")]
        public async Task<IActionResult> GetAllReviews(GetAllReviewRequestModel model)
        {
            try
            {
                var result = await _reviewService.GetAllReviews(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewById(int id)
        {
            try
            {
                var result = await _reviewService.GetReviewById(id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPost]
        public async Task<IActionResult> CreateReview(CreateReviewRequestModel model)
        {
            try
            {
                var result = await _reviewService.CreateReviewAsync(model);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw new Exception("Error: ",ex);
            }
        }

        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        public async Task<IActionResult> UpdateReview([FromBody]CreateReviewRequestModel model, int id)
        {
            try
            {
                var result = await _reviewService.UpdateReviewAsync(model, id);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteReview(int id, bool status)
        {
            try
            {
                var result = await _reviewService.DeleteReviewAsync(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = "Admin,Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReviewAsync(int id, bool status)
        {
            try
            {
                var result = await _reviewService.DeleteReviewAsync(id,status);
                return StatusCode(result.Code, result);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
