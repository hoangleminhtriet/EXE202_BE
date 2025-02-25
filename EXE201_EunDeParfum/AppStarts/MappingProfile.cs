using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Answer;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.RequestModel.Question;
using EunDeParfum_Service.RequestModel.Result;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.RequestModel.UserAnswer;
using EunDeParfum_Service.ResponseModel.Answer;
using EunDeParfum_Service.ResponseModel.Customer;
using EunDeParfum_Service.ResponseModel.Question;
using EunDeParfum_Service.ResponseModel.Result;
using EunDeParfum_Service.ResponseModel.Review;
using EunDeParfum_Service.ResponseModel.UserAnswer;

namespace EXE201_EunDeParfum.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            //Customer
            CreateMap<RegisterRequestModel, Customer>().ReverseMap();
            CreateMap<CustomerResponseModel, Customer>().ReverseMap();
            CreateMap<RegisterRequestModel, CustomerResponseModel>().ReverseMap();
            CreateMap<UpdateRequestModel, Customer>().ReverseMap();

            //Review
            CreateMap<CreateReviewRequestModel, Review>().ReverseMap();
            CreateMap<CreateReviewRequestModel, ReviewResponseModel>().ReverseMap();
            CreateMap<CreateReviewRequestModel, Review>().ReverseMap();
            CreateMap<ReviewResponseModel, Review>().ReverseMap();

            //Answer
            CreateMap<CreateAnswerRequestModel, Answer>().ReverseMap();
            CreateMap<CreateAnswerRequestModel, ReviewResponseModel>().ReverseMap();
            CreateMap<CreateAnswerRequestModel, Answer>().ReverseMap();
            CreateMap<AnswerResponseModel, Answer>().ReverseMap();

            //UserAnswer
            CreateMap<CreateUserAnswerRequestModel, UserAnswer>().ReverseMap();
            CreateMap<CreateUserAnswerRequestModel, UserAnswerResponseModel>().ReverseMap();
            CreateMap<CreateUserAnswerRequestModel, UserAnswer>().ReverseMap();
            CreateMap<UserAnswerResponseModel, UserAnswer>().ReverseMap();

            //Result
            CreateMap<CreateResultRequestModel, Result>().ReverseMap();
            CreateMap<CreateResultRequestModel, ResultResponseModel>().ReverseMap();
            CreateMap<CreateResultRequestModel, Result>().ReverseMap();
            CreateMap<ResultResponseModel, Result>().ReverseMap();

            //Question
            CreateMap<CreateQuestionRequestModel, Question>().ReverseMap();
            CreateMap<CreateQuestionRequestModel, QuestionResponseModel>().ReverseMap();
            CreateMap<CreateQuestionRequestModel, Question>().ReverseMap();
            CreateMap<QuestionResponseModel, Question>().ReverseMap();
        }
    }
}
