using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Result;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Question;
using EunDeParfum_Service.ResponseModel.Result;
using EunDeParfum_Service.ResponseModel.Review;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class ResultsService : IResultService
    {
        private readonly IResultRepository _resultRepository;
        private readonly IMapper _mapper;
        public ResultsService(IResultRepository resultRepository, IMapper mapper)
        {
            _resultRepository = resultRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<ResultResponseModel>> CreateResultAsync(CreateResultRequestModel model)
        {
            try
            {
                var result = _mapper.Map<Result>(model);
                result.Status = true;
                await _resultRepository.CreateResultAsync(result);
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Result success!.",
                    Data = _mapper.Map<ResultResponseModel>(result)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ResultResponseModel>> DeleteResultAsync(int resultId, bool status)
        {
            try
            {
                var result = await _resultRepository.GetResultByIdAsync(resultId);
                if (result == null)
                {
                    return new BaseResponse<ResultResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Result!.",
                        Data = null
                    };
                }
                result.Status = status;
                await _resultRepository.UpdateResultAsync(result);
                return new BaseResponse<ResultResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ResultResponseModel>(result)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ResultResponseModel>> GetAllResults(GetAllResultRequestModel model)
        {
            try
            {
                var listResult = await _resultRepository.GetAllResultAsync();
                if (model.Status != null)
                {
                    listResult = listResult.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<ResultResponseModel>>(listResult);

                var pageResult = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<ResultResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<ResultResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageResult.PageNumber,
                            Size = pageResult.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageResult.PageCount,
                            TotalItem = pageResult.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageResult.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<ResultResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ResultResponseModel>> GetResultByIdAsync(int resultId)
        {
            try
            {
                var result = await _resultRepository.GetResultByIdAsync(resultId);
                if (resultId == null)
                {
                    return new BaseResponse<ResultResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Result!.",
                        Data = null
                    };
                }
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ResultResponseModel>(result)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ResultResponseModel>> UpdateResultAsync(CreateResultRequestModel model, int id)
        {
            try
            {
                var result = await _resultRepository.GetResultByIdAsync(id);
                if (result == null)
                {
                    return new BaseResponse<ResultResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Result!.",
                        Data = null
                    };
                }
                await _resultRepository.UpdateResultAsync(_mapper.Map(model, result));
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Question Success!.",
                    Data = _mapper.Map<ResultResponseModel>(result)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ResultResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }
    }
}
