using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IReviewService
    {
        Task<BaseResponse<ReviewResponseModel>> CreateReviewAsync(CreateReviewRequestModel model);
        Task<BaseResponse<ReviewResponseModel>> UpdateReviewAsync(CreateReviewRequestModel model, int id);
        Task<BaseResponse<ReviewResponseModel>> DeleteReviewAsync(int reviewId, bool status);
        Task<BaseResponse<ReviewResponseModel>> GetReviewById(int reviewId);
        Task<DynamicResponse<ReviewResponseModel>> GetAllReviews(GetAllReviewRequestModel model);
    }
}
