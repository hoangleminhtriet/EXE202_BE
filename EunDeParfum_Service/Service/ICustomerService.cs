using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.Service
{
    public interface ICustomerService
    {
        public Task<BaseResponse<CustomerResponseModel>> RegisterCustomerByEmail(string googleId);
        public Task<BaseResponse<CustomerResponseModel>> CreateAccountAdmin(string email, string password, string name);
        public Task<BaseResponse<CustomerResponseModel>> CreateAccountStaff(string email, string password, string name);
        public Task<BaseResponse<CustomerResponseModel>> CreateAccountManager(string email, string password, string name);
        public Task<BaseResponse<CustomerResponseModel>> RegisterCustomer(RegisterRequestModel model);
        Task<BaseResponse> SendMailWithoutPassword(string email);
        Task<BaseResponse> SendMailWithPassword(string email, string password);
        Task<BaseResponse> VerifyAcccount(int id);
        Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model);
        Task<BaseResponse<LoginResponseModel>> LoginMail(string googleId);
        Task<DynamicResponse<CustomerResponseModel>> GetListCustomer(GetAllCustomerRequestModel model);
        Task<BaseResponse<CustomerResponseModel>> GetCustomerById(int id);
        Task<BaseResponse<CustomerResponseModel>> UpdateCustomer(int id, UpdateRequestModel model);
        Task<BaseResponse<CustomerResponseModel>> DeleteCustomer(int id, bool status);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
        public Task<BaseResponse> ChangePassword(int id, string currentPassword, string newPassword);
        Task<BaseResponse> ResendVerificationEmail(string email);
        Task<BaseResponse> BlockCustomer(int customerId);
    }
}
