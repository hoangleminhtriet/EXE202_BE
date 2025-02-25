using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Implement;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.UserAnswer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Review;
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
    public class UserAnswersService : IUserAnswersService
    {
        private readonly IUserAnswerRepository _userAnswerRepository;
        private readonly IMapper _mapper;
        public UserAnswersService(IMapper mapper, IUserAnswerRepository userAnswerRepository)
        {
            _mapper = mapper;
            _userAnswerRepository = userAnswerRepository;
        }
        public async Task<BaseResponse<UserAnswerResponseModel>> CreateUserAnswerAsync(CreateUserAnswerRequestModel model)
        {
            try
            {
                var userAnswer = _mapper.Map<UserAnswer>(model);
                userAnswer.Status = true;
                await _userAnswerRepository.CreateUserAnswerAsync(userAnswer);
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create UserAnswer success",
                    Data = _mapper.Map<UserAnswerResponseModel>(model)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<UserAnswerResponseModel>> DeleteUserAnswerAsync(int userAnswerId, bool status)
        {
            try
            {
                var userAnswer = await _userAnswerRepository.GetUserAnswerByIdAsync(userAnswerId);
                if (userAnswer == null)
                {
                    return new BaseResponse<UserAnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found UserAnswer!.",
                        Data = null
                    };
                }
                userAnswer.Status = status;
                await _userAnswerRepository.UpdateUserAnswerAsync(userAnswer);
                return new BaseResponse<UserAnswerResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<UserAnswerResponseModel>(userAnswer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<UserAnswerResponseModel>> GetAllUserAnswers(GetAllUserAnswerRequestModel model)
        {
            try
            {
                var listUserAnswer = await _userAnswerRepository.GetAllUserAnswersAsync();
                if (model.Status != null)
                {
                    listUserAnswer = listUserAnswer.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<UserAnswerResponseModel>>(listUserAnswer);

                var pageUserAnswer = result
                    .OrderBy(c => c.Id)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<UserAnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<UserAnswerResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageUserAnswer.PageNumber,
                            Size = pageUserAnswer.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageUserAnswer.PageCount,
                            TotalItem = pageUserAnswer.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageUserAnswer.ToList()
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<UserAnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<UserAnswerResponseModel>> GetUserAnswerByIdAsync(int userAnswerId)
        {
            try
            {
                var userAnswer = await _userAnswerRepository.GetUserAnswerByIdAsync(userAnswerId);
                if (userAnswerId == null)
                {
                    return new BaseResponse<UserAnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found UserAnswer!.",
                        Data = null
                    };
                }
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<UserAnswerResponseModel>(userAnswer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<UserAnswerResponseModel>> UpdateUserAnswerAsync(CreateUserAnswerRequestModel model, int id)
        {
            try
            {
                var userAnswer = await _userAnswerRepository.GetUserAnswerByIdAsync(id);
                if (userAnswer == null)
                {
                    return new BaseResponse<UserAnswerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount UserAnswer!.",
                        Data = null
                    };
                }
                await _userAnswerRepository.UpdateUserAnswerAsync(_mapper.Map(model, userAnswer));
                return new BaseResponse<UserAnswerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update UserAnswer Success!.",
                    Data = _mapper.Map<UserAnswerResponseModel>(userAnswer)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<UserAnswerResponseModel>()
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
