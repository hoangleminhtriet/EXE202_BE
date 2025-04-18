﻿using AutoMapper;
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
using EunDeParfum_Service.ResponseModel.ProductRecommendation;
using EunDeParfum_Service.RequestModel.Order;
using EunDeParfum_Service.ResponseModel.OrderDetail;
using EunDeParfum_Service.ResponseModel.Order;
using EunDeParfum_Service.RequestModel.VIETQR;
using EunDeParfum_Service.ResponseModel.VIETQR;
using EunDeParfum_Service.RequestModel.Payment;
using EunDeParfum_Service.ResponseModel.Payment;  // Thêm dòng này để sử dụng Category

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

            //Order
            CreateMap<CreateOrderRequestModel, Order>().ReverseMap();
            CreateMap<CreateOrderRequestModel, OrderReponseModel>().ReverseMap();
            CreateMap<OrderReponseModel, Order>().ReverseMap();
            CreateMap<UpdateOrderRequestModel, Order>().ReverseMap();
            CreateMap<AddToCartRequestModel, Order>().ReverseMap();
            CreateMap<UpdateCartRequestModel, Order>().ReverseMap();
            CreateMap<RemoveCartItemsRequestModel, Order>().ReverseMap();

            //OrderDetail
            CreateMap<OrderDetailResponseModel, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail, OrderDetailResponseModel>()
                .ForMember(dest => dest.ProductName, opt => opt.Ignore());
            CreateMap<ProductForOrderRequestModel, OrderDetail>()
            .ForMember(dest => dest.OrderId, opt => opt.Ignore())
            .ForMember(dest => dest.OrderDetailId, opt => opt.Ignore());


            // Ánh xạ VietQrRequest → VietQrResponse
            CreateMap<VietQrRequest, VietQrResponse>()
                .ForMember(dest => dest.Success, opt => opt.Ignore())
                .ForMember(dest => dest.QrBase64, opt => opt.Ignore())
                .ForMember(dest => dest.Message, opt => opt.Ignore());

            // Ánh xạ CreatePaymentRequestModel → Payment
            CreateMap<CreatePaymentRequestModel, Payment>()
                .ForMember(dest => dest.PaymentId, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.TransactionId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore());

            // Ánh xạ Payment → PaymentResponseModel
            CreateMap<Payment, PaymentResponseModel>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Order.CustomerId))
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Order.TotalAmount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(src => src.TransactionId))
                .ForMember(dest => dest.CheckoutUrl, opt => opt.Ignore());

            // Ánh xạ ngược PaymentResponseModel → Payment (nếu cần)
            CreateMap<PaymentResponseModel, Payment>()
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.PaymentDate, opt => opt.Ignore());

        }
    }
}
