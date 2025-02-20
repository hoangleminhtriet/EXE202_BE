using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.RequestModel.Review;
using EunDeParfum_Service.ResponseModel.Customer;
using EunDeParfum_Service.ResponseModel.Review;

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
        }
    }
}
