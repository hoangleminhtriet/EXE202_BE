using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Question;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Question;
using EunDeParfum_Service.ResponseModel.UserAnswer;
using EunDeParfum_Service.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

namespace EunDeParfum_Service.Service.Implement
{
    public class QuestionsService : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        public QuestionsService(IQuestionRepository questionRepository, IMapper mapper)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
        }
        public async Task<BaseResponse<QuestionResponseModel>> CreateQuestionAsync(CreateQuestionRequestModel model)
        {
            try
            {
                var question = _mapper.Map<Question>(model);
                question.Status = true;
                await _questionRepository.CreateQuestionAsync(question);
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Question success",
                    Data = _mapper.Map<QuestionResponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<QuestionResponseModel>> DeleteQuestionAsync(int questionId, bool status)
        {
            try
            {
                var question = await _questionRepository.GetQuestionByIdAsync(questionId);
                if (question == null)
                {
                    return new BaseResponse<QuestionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Question!.",
                        Data = null
                    };
                }
                question.Status = status;
                await _questionRepository.UpdateQuestionAsync(question);
                return new BaseResponse<QuestionResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<QuestionResponseModel>(question)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<QuestionResponseModel>> GetAllQuestions(GetAllQuestionRequestModel model)
        {
            try
            {
                var listQuestion = await _questionRepository.GetAllQuestionsAsync();
                if (model.Status != null)
                {
                    listQuestion = listQuestion.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<QuestionResponseModel>>(listQuestion);

                var pageQuestion = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<QuestionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<QuestionResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageQuestion.PageNumber,
                            Size = pageQuestion.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageQuestion.PageCount,
                            TotalItem = pageQuestion.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageQuestion.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<QuestionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<QuestionResponseModel>> GetQuestionById(int questionId)
        {
            try
            {
                var question = await _questionRepository.GetQuestionByIdAsync(questionId);
                if (questionId == null)
                {
                    return new BaseResponse<QuestionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Question!.",
                        Data = null
                    };
                }
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<QuestionResponseModel>(question)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<QuestionResponseModel>> UpdateQuestionAsync(CreateQuestionRequestModel model, int id)
        {
            try
            {
                var question = await _questionRepository.GetQuestionByIdAsync(id);
                if (question == null)
                {
                    return new BaseResponse<QuestionResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Question!.",
                        Data = null
                    };
                }
                await _questionRepository.UpdateQuestionAsync(_mapper.Map(model, question));
                return new BaseResponse<QuestionResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Question Success!.",
                    Data = _mapper.Map<QuestionResponseModel>(question)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<QuestionResponseModel>()
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
