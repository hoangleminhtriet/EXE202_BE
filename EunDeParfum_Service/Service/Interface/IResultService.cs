using EunDeParfum_Service.RequestModel.Result;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Result;
using EunDeParfum_Service.ResponseModel.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service.Interface
{
    public interface IResultService
    {
        Task<BaseResponse<ResultResponseModel>> CreateResultAsync(CreateResultRequestModel model);
        Task<BaseResponse<ResultResponseModel>> UpdateResultAsync(CreateResultRequestModel model, int id);
        Task<BaseResponse<ResultResponseModel>> DeleteResultAsync(int resultId, bool status);
        Task<BaseResponse<ResultResponseModel>> GetResultByIdAsync(int resultId);
        Task<DynamicResponse<ResultResponseModel>> GetAllResults(GetAllResultRequestModel model);

    }
}
