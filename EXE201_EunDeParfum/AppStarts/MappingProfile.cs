using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.ResponseModel.Customer;

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
        }
    }
}
