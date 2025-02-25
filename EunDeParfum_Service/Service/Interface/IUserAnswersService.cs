using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.RequestModel.UserAnswer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Review;
using EunDeParfum_Service.ResponseModel.UserAnswer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IUserAnswersService
    {
        Task<BaseResponse<UserAnswerResponseModel>> CreateUserAnswerAsync(CreateUserAnswerRequestModel model);
        Task<BaseResponse<UserAnswerResponseModel>> UpdateUserAnswerAsync(CreateUserAnswerRequestModel model, int id);
        Task<BaseResponse<UserAnswerResponseModel>> DeleteUserAnswerAsync(int userAnswerId, bool status);
        Task<BaseResponse<UserAnswerResponseModel>> GetUserAnswerByIdAsync(int userAnswerId);
        Task<DynamicResponse<UserAnswerResponseModel>> GetAllUserAnswers(GetAllUserAnswerRequestModel model);

    }
}
