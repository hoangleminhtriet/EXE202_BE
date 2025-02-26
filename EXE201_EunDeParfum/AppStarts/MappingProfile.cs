using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Answer;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.RequestModel.Product;
using EunDeParfum_Service.RequestModel.ProductCategory;
using EunDeParfum_Service.RequestModel.Question;
using EunDeParfum_Service.RequestModel.Result;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.RequestModel.UserAnswer;
using EunDeParfum_Service.RequestModel.Category;  // Thêm dòng này để sử dụng Category
using EunDeParfum_Service.ResponseModel.Answer;
using EunDeParfum_Service.ResponseModel.Customer;
using EunDeParfum_Service.ResponseModel.Product;
using EunDeParfum_Service.ResponseModel.ProductCategory;
using EunDeParfum_Service.ResponseModel.Question;
using EunDeParfum_Service.ResponseModel.Result;
using EunDeParfum_Service.ResponseModel.Review;
using EunDeParfum_Service.ResponseModel.UserAnswer;
using EunDeParfum_Service.ResponseModel;
using EunDeParfum_Service.RequestModel.ProductRecommendation;
using EunDeParfum_Service.ResponseModel.ProductRecommendation;  // Thêm dòng này để sử dụng Category

namespace EXE201_EunDeParfum.AppStarts
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Customer
            CreateMap<RegisterRequestModel, Customer>().ReverseMap();
            CreateMap<CustomerResponseModel, Customer>().ReverseMap();
            CreateMap<RegisterRequestModel, CustomerResponseModel>().ReverseMap();
            CreateMap<UpdateRequestModel, Customer>().ReverseMap();

            // Review
            CreateMap<CreateReviewRequestModel, Review>().ReverseMap();
            CreateMap<CreateReviewRequestModel, ReviewResponseModel>().ReverseMap();
            CreateMap<CreateReviewRequestModel, Review>().ReverseMap();
            CreateMap<ReviewResponseModel, Review>().ReverseMap();

            // Answer
            CreateMap<CreateAnswerRequestModel, Answer>().ReverseMap();
            CreateMap<CreateAnswerRequestModel, ReviewResponseModel>().ReverseMap();
            CreateMap<CreateAnswerRequestModel, Answer>().ReverseMap();
            CreateMap<AnswerResponseModel, Answer>().ReverseMap();

            // UserAnswer
            CreateMap<CreateUserAnswerRequestModel, UserAnswer>().ReverseMap();
            CreateMap<CreateUserAnswerRequestModel, UserAnswerResponseModel>().ReverseMap();
            CreateMap<CreateUserAnswerRequestModel, UserAnswer>().ReverseMap();
            CreateMap<UserAnswerResponseModel, UserAnswer>().ReverseMap();

            // Result
            CreateMap<CreateResultRequestModel, Result>().ReverseMap();
            CreateMap<CreateResultRequestModel, ResultResponseModel>().ReverseMap();
            CreateMap<CreateResultRequestModel, Result>().ReverseMap();
            CreateMap<ResultResponseModel, Result>().ReverseMap();

            // Question
            CreateMap<CreateQuestionRequestModel, Question>().ReverseMap();
            CreateMap<CreateQuestionRequestModel, QuestionResponseModel>().ReverseMap();
            CreateMap<CreateQuestionRequestModel, Question>().ReverseMap();
            CreateMap<QuestionResponseModel, Question>().ReverseMap();

            // Product
            CreateMap<CreateProductRequestModel, Product>().ReverseMap();
            CreateMap<CreateProductRequestModel, ProductResponseModel>().ReverseMap();
            CreateMap<CreateProductRequestModel, Product>().ReverseMap();
            CreateMap<ProductResponseModel, Product>().ReverseMap();

            // ProductCategory (Thêm mapping mới)
            CreateMap<CreateProductCategoryRequestModel, ProductCategory>().ReverseMap();
            CreateMap<UpdateProductCategoryRequestModel, ProductCategory>().ReverseMap();
            CreateMap<ProductCategoryResponseModel, ProductCategory>().ReverseMap();

            // Category (Thêm mapping mới)
            CreateMap<CreateCategoryRequestModel, Category>().ReverseMap();
            CreateMap<CreateCategoryRequestModel, Category>().ReverseMap();
            CreateMap<CategoryResponseModel, Category>().ReverseMap();

            //ProductCommendation
            CreateMap<CreateProductRecommendationRequestModel, ProductRecommendation>().ReverseMap();
            CreateMap<ProductRecommendationResponseModel, ProductRecommendation>().ReverseMap();
            
        }
    }
}
