using AutoMapper;
using AutoMapper.Internal;
using EunDeParfum_Repository.Models;
using EunDeParfum_Repository.Repository.Interface;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.BaseResponse;
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
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponse<ReviewResponseModel>> CreateReviewAsync(CreateReviewRequestModel model)
        {
            try
            {
                var review = _mapper.Map<Review>(model);
                review.Status = true;
                await _reviewRepository.CreateReviewAsync(review);
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Create Review success!.",
                    Data = _mapper.Map<ReviewResponseModel>(review)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ReviewResponseModel>> DeleteReviewAsync(int reviewId, bool status)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
                if (review == null)
                {
                    return new BaseResponse<ReviewResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Review!.",
                        Data = null
                    };
                }
                review.Status = status;
                await _reviewRepository.UpdateReviewAsync(review);
                return new BaseResponse<ReviewResponseModel>
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ReviewResponseModel>(review)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<DynamicResponse<ReviewResponseModel>> GetAllReviews(GetAllReviewRequestModel model)
        {
            try
            {
                var listReview = await _reviewRepository.GetAllReviewsAsync();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Review> listReviewByRating = listReview.Where(r => r.Rating.ToString().Contains(model.keyWord)).ToList();
                    List<Review> listReviewByComment = listReview.Where(c => c.Comment.ToLower().Contains(model.keyWord)).ToList();
                    listReview = listReviewByRating
                        .Concat(listReviewByComment)
                        .GroupBy(a => a.ReviewId)
                        .Select(g => g.First())
                        .ToList();
                }
                if(model.Status != null)
                {
                    listReview = listReview.Where(c => c.Status == model.Status).ToList();
                }
                var result = _mapper.Map<List<ReviewResponseModel>>(listReview);

                var pageReview = result
                    .OrderBy(c => c.ReviewId)
                    .ToPagedList(model.pageNum, model.pageSize);
                return new DynamicResponse<ReviewResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<ReviewResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pageReview.PageNumber,
                            Size = pageReview.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pageReview.PageCount,
                            TotalItem = pageReview.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = null,
                            status = model.Status,
                            is_Verify = null,
                            is_Delete = null
                        },
                        PageData = pageReview.ToList()
                    },
                };
            }
            catch(Exception ex)
            {
                return new DynamicResponse<ReviewResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ReviewResponseModel>> GetReviewById(int reviewId)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(reviewId);
                if (review == null)
                {
                    return new BaseResponse<ReviewResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Review!.",
                        Data = null
                    };
                }
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,
                    Data = _mapper.Map<ReviewResponseModel>(review)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!",
                    Data = null
                };
            }
        }

        public async Task<BaseResponse<ReviewResponseModel>> UpdateReviewAsync(CreateReviewRequestModel model, int id)
        {
            try
            {
                var review = await _reviewRepository.GetReviewByIdAsync(id);
                if (review == null)
                {
                    return new BaseResponse<ReviewResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not fount Review!.",
                        Data = null
                    };
                }
                await _reviewRepository.UpdateReviewAsync(_mapper.Map(model, review));
                return new BaseResponse<ReviewResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = "Update Review Success!.",
                    Data = _mapper.Map<ReviewResponseModel>(review)
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ReviewResponseModel>()
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
