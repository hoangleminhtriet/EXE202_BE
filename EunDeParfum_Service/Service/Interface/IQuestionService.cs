using EunDeParfum_Service.RequestModel.Question;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Question;
using EunDeParfum_Service.ResponseModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IQuestionService
    {
        Task<BaseResponse<QuestionResponseModel>> CreateQuestionAsync(CreateQuestionRequestModel model);
        Task<BaseResponse<QuestionResponseModel>> UpdateQuestionAsync(CreateQuestionRequestModel model, int id);
        Task<BaseResponse<QuestionResponseModel>> DeleteQuestionAsync(int questionId, bool status);
        Task<BaseResponse<QuestionResponseModel>> GetQuestionById(int questionId);
        Task<DynamicResponse<QuestionResponseModel>> GetAllQuestions(GetAllQuestionRequestModel model);
    }
}
