using EunDeParfum_Service.RequestModel.Answer;
using EunDeParfum_Service.ResponseModel.Answer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IAnswersService
    {
        Task<BaseResponse<AnswerResponseModel>> CreateAnswerAsync(CreateAnswerRequestModel model);
        Task<BaseResponse<AnswerResponseModel>> UpdateAnswerAsync(CreateAnswerRequestModel model, int id);
        Task<BaseResponse<AnswerResponseModel>> DeleteAnswerAsync(int answerId, bool status);
        Task<BaseResponse<AnswerResponseModel>> GetAnswerByIdAsync(int answerId);
        Task<DynamicResponse<AnswerResponseModel>> GetAllAnswers(GetAllAnswerRequestModel model);
    }
}
