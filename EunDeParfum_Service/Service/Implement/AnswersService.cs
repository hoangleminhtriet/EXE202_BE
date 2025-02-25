using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Answer;
using EunDeParfum_Service.ResponseModel.Answer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Question;
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
    public class AnswersService : IAnswersService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        public AnswersService(IAnswerRepository answerRepository, IMapper mapper)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<AnswerResponseModel>> CreateAnswerAsync(CreateAnswerRequestModel model)
        {
            try
            {
                var answer = _mapper.Map<Answer>(model);
                answer.Status = true;
                await _answerRepository.CreateAnswerAsync(answer);
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Answer success!.",
                    Data = _mapper.Map<AnswerResponseModel>(answer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<AnswerResponseModel>> DeleteAnswerAsync(int answerId, bool status)
        {
            try
            {
                var answer = await _answerRepository.GetAnswerByIdAsync(answerId);
                if (answer == null)
                {
                    return new BaseResponse<AnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Answer!.",
                        Data = null
                    };
                }
                answer.Status = status;
                await _answerRepository.UpdateAnswerAsync(answer);
                return new BaseResponse<AnswerResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<AnswerResponseModel>(answer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<AnswerResponseModel>> GetAllAnswers(GetAllAnswerRequestModel model)
        {
            try
            {
                var listAnswer = await _answerRepository.GetAllAnswersAsync();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Answer> listAnswerByText = listAnswer.Where(a => a.AnswerText.ToLower().Contains(model.keyWord)).ToList();
                    listAnswer = listAnswerByText
                        .GroupBy(b => b.Id)
                        .Select(g => g.First())
                        .ToList();
                }
                if (model.Status != null)
                {
                    listAnswer = listAnswer.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<AnswerResponseModel>>(listAnswer);

                var pageAnswer = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<AnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<AnswerResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageAnswer.PageNumber,
                            Size = pageAnswer.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageAnswer.PageCount,
                            TotalItem = pageAnswer.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageAnswer.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<AnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<AnswerResponseModel>> GetAnswerByIdAsync(int answerId)
        {
            try
            {
                var answer = await _answerRepository.GetAnswerByIdAsync(answerId);
                if (answer == null)
                {
                    return new BaseResponse<AnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Answer!.",
                        Data = null
                    };
                }
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<AnswerResponseModel>(answer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<AnswerResponseModel>> UpdateAnswerAsync(CreateAnswerRequestModel model, int id)
        {
            try
            {
                var answer = await _answerRepository.GetAnswerByIdAsync(id);
                if (answer == null)
                {
                    return new BaseResponse<AnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Answer!.",
                        Data = null
                    };
                }
                await _answerRepository.UpdateAnswerAsync(_mapper.Map(model, answer));
                return new BaseResponse<AnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Answer Success!.",
                    Data = _mapper.Map<AnswerResponseModel>(answer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AnswerResponseModel>()
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
