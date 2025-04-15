using AutoMapper;
using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.ResponseModel.Customer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using X.PagedList;
using EunDeParfum_Service.Service.Interface;
using EunDeParfum_Repository.Repository.Interface;

namespace EunDeParfum_Service.Service.Implement
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public CustomerService(ICustomerRepository customerRepository, IConfiguration configuration, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
            _mapper = mapper;
        }
        public async Task<BaseResponse> BlockCustomer(int customerId)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(customerId);
                if (customer == null)
                {
                    return new BaseResponse()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Customer!."
                    };
                }
                else
                {
                    customer.Status = true;
                    await _customerRepository.UpdateCustomer(customer);
                    return new BaseResponse()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Block Customer sucessful!."
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server error!."
                };
            }
        }

        public async Task<BaseResponse> ChangePassword(int id, string currentPassword, string newPassword)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);

                if (customer == null)
                {
                    return new BaseResponse
                    {
                        Code = 404,
                        Success = false,
                        Message = "Customer not found!"
                    };
                }

                if (!VerifyPassword(currentPassword, customer.Password))
                {
                    return new BaseResponse
                    {
                        Code = 401,
                        Success = false,
                        Message = "Current password is incorrect!"
                    };
                }

                string hashedNewPassword = HashPassword(newPassword);


                customer.Password = hashedNewPassword;
                customer.ModifiedDate = DateTime.Now;

                bool updateSuccess = await _customerRepository.UpdateCustomer(customer);

                if (updateSuccess)
                {
                    return new BaseResponse
                    {
                        Code = 200,
                        Success = true,
                        Message = "Password changed successfully."
                    };
                }
                else
                {
                    return new BaseResponse
                    {
                        Code = 500,
                        Success = false,
                        Message = "Failed to update password due to a server error."
                    };
                }

            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 500,
                    Success = false,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> CreateAccountAdmin(string email, string password, string name)
        {
            try
            {
                Customer checkExit = await _customerRepository.GetCustomerByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "Customer has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                Customer customer = new Customer()
                {
                    Email = email,
                    Name = name,
                    Status = true,
                    Gender = "Female",
                    Phone = "09238298322",
                    Address = "HCM",
                    ModifiedDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Admin",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _customerRepository.CreateCustomer(customer);
                if (!check)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<CustomerResponseModel>(customer);
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register admin success!.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = $"Server Error! {ex.Message}"

                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> CreateAccountManager(string email, string password, string name)
        {
            try
            {
                Customer checkExit = await _customerRepository.GetCustomerByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "Customer has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                Customer customer = new Customer()
                {
                    Email = email,
                    Name = name,
                    Status = true,
                    Gender = "Male",
                    Phone = "09238298322",
                    Address = "HCM",
                    ModifiedDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Manager",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _customerRepository.CreateCustomer(customer);
                if (!check)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<CustomerResponseModel>(customer);
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register manager success!.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> CreateAccountStaff(string email, string password, string name)
        {
            try
            {
                Customer checkExit = await _customerRepository.GetCustomerByEmail(email);
                if (checkExit != null)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "Customer has been exits!"
                    };
                }
                string hashPassword = HashPassword(password);
                Customer customer = new Customer()
                {
                    Email = email,
                    Name = name,
                    Status = true,
                    Gender = "Male",
                    Phone = "09238298321",
                    Address = "HCM",
                    ModifiedDate = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    Password = hashPassword,
                    RoleName = "Staff",
                    IsVerify = true,
                    IsDelete = false,
                };
                bool check = await _customerRepository.CreateCustomer(customer);
                if (!check)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                var response = _mapper.Map<CustomerResponseModel>(customer);
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register staff success!.",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> DeleteCustomer(int id, bool status)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer != null)
                {
                    customer.ModifiedDate = DateTime.Now;
                    customer.IsDelete = status;
                    await _customerRepository.UpdateCustomer(customer);
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Delete success!.",
                        Data = _mapper.Map<CustomerResponseModel>(customer)
                    };
                }
                else
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Customer!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer != null)
                {
                    var result = _mapper.Map<CustomerResponseModel>(customer);
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = null,
                        Data = result
                    };
                }
                else
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found Customer!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<DynamicResponse<CustomerResponseModel>> GetListCustomer(GetAllCustomerRequestModel model)
        {
            try
            {
                var listCustomer = await _customerRepository.GetAllCustomers();
                if (!string.IsNullOrEmpty(model.keyWord))
                {
                    List<Customer> listCustomerByName = listCustomer.Where(u => u.Name.Contains(model.keyWord)).ToList();

                    List<Customer> listCustomerByEmail = listCustomer.Where(u => u.Email.Contains(model.keyWord)).ToList();

                    listCustomer = listCustomerByName
                               .Concat(listCustomerByEmail)
                               .GroupBy(u => u.CustomerId)
                               .Select(g => g.First())
                               .ToList();
                }
                if (!string.IsNullOrEmpty(model.role))
                {
                    if (!model.role.Equals("ALL") && !model.role.Equals("all") && !model.role.Equals("All"))
                    {
                        listCustomer = listCustomer.Where(u => u.RoleName.Equals(model.role)).ToList();
                    }
                }
                if (model.status != null)
                {
                    listCustomer = listCustomer.Where(u => u.Status == model.status).ToList();
                }
                if (model.is_Verify != null)
                {
                    listCustomer = listCustomer.Where(u => u.IsVerify == model.is_Verify).ToList();
                }
                if (model.is_Delete != null)
                {
                    listCustomer = listCustomer.Where(u => u.IsDelete == model.is_Delete).ToList();
                }
                var result = _mapper.Map<List<CustomerResponseModel>>(listCustomer);

                // Nếu không có lỗi, thực hiện phân trang
                var pagedCustomers = result// Giả sử result là danh sách người dùng
                    .OrderBy(u => u.CustomerId) // Sắp xếp theo Id tăng dần
                    .ToPagedList(model.pageNum, model.pageSize); // Phân trang với X.PagedList
                return new DynamicResponse<CustomerResponseModel>()
                {
                    Code = 200,
                    Success = true,
                    Message = null,

                    Data = new MegaData<CustomerResponseModel>()
                    {
                        PageInfo = new PagingMetaData()
                        {
                            Page = pagedCustomers.PageNumber,
                            Size = pagedCustomers.PageSize,
                            Sort = "Ascending",
                            Order = "Id",
                            TotalPage = pagedCustomers.PageCount,
                            TotalItem = pagedCustomers.TotalItemCount,
                        },
                        SearchInfo = new SearchCondition()
                        {
                            keyWord = model.keyWord,
                            role = model.role,
                            status = model.status,
                            is_Verify = model.is_Verify,
                            is_Delete = model.is_Delete,
                        },
                        PageData = pagedCustomers.ToList(),
                    },
                };
            }
            catch (Exception ex)
            {
                return new DynamicResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = null,
                    Data = null,
                };
            }
        }

        public string HashPassword(string password)
        {
            try
            {
                byte[] salt = new byte[16];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(salt);
                }

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] hash = pbkdf2.GetBytes(20);

                byte[] hashBytes = new byte[36];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 20);
                string hashedPassword = Convert.ToBase64String(hashBytes);

                return hashedPassword;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GeneratePassword()
        {
            try
            {
                string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()";
                var bytes = new byte[8];
                using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(bytes);
                }
                var password = new string(bytes.Select(b => characters[b % characters.Length]).ToArray());
                return password;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmail(model.Email);
                if (customer != null)
                {
                    if (VerifyPassword(model.Password, customer.Password))
                    {
                        if (customer.IsVerify == false)
                        {
                            return new BaseResponse<LoginResponseModel>()
                            {
                                Code = 401,
                                Success = false,
                                Message = "Email not verified!.",
                                Data = null
                            };
                        }

                        if (customer.IsDelete == true)
                        {
                            return new BaseResponse<LoginResponseModel>()
                            {
                                Code = 401,
                                Success = false,
                                Message = "Customer has been delete!.",
                                Data = null,
                            };
                        }

                        if (customer.Status == false)
                        {
                            return new BaseResponse<LoginResponseModel>()
                            {
                                Code = 401,
                                Success = false,
                                Message = "Customer has been block!.",
                                Data = null,
                            };
                        }
                        string token = GenerateJwtToken(customer.Name, customer.RoleName, customer.CustomerId);
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 200,
                            Success = true,
                            Message = "Login success!",
                            Data = new LoginResponseModel()
                            {
                                token = token,
                                customer = _mapper.Map<CustomerResponseModel>(customer)
                            },
                        };
                    }
                    else
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 404,
                            Success = false,
                            Message = "Incorrect Customer or password!"
                        };
                    }
                }
                else
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Incorrect Customer or password!"
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoginResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = $"Server Error: {ex.Message} | {ex.InnerException?.Message}"

                };
            }
        }

        public async Task<BaseResponse<LoginResponseModel>> LoginMail(string googleId)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleId);
                var email = payload.Email;
                var customer = await _customerRepository.GetCustomerByEmail(email);
                if (customer != null)
                {
                    if (customer.IsVerify == false)
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "Email not verified!.",
                            Data = null,
                        };
                    }

                    if (customer.IsDelete == true)
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "Customer has been delete!.",
                            Data = null,
                        };
                    }
                    if (customer.Status == false)
                    {
                        return new BaseResponse<LoginResponseModel>()
                        {
                            Code = 401,
                            Success = false,
                            Message = "Customer has been block!.",
                            Data = null,
                        };
                    }

                    string token = GenerateJwtToken(customer.Name, customer.RoleName, customer.CustomerId);
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Login success!",
                        Data = new LoginResponseModel()
                        {
                            token = token,
                            customer = _mapper.Map<CustomerResponseModel>(customer)
                        },
                    };
                }
                else
                {
                    return new BaseResponse<LoginResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = null,
                        Data = null,
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<LoginResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!.",
                    Data = null,
                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> RegisterCustomer(RegisterRequestModel model)
        {
            try
            {
                Customer checkExit = await _customerRepository.GetCustomerByEmail(model.Email);
                if (checkExit != null)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "Customer has been exits!"
                    };
                }
                string hashPassword = HashPassword(model.Password);
                var Customer = _mapper.Map<Customer>(model);
                Customer.Status = true;
                Customer.IsDelete = false;
                Customer.IsVerify = false;
                Customer.CreatedAt = DateTime.Now;
                Customer.Password = hashPassword;
                Customer.RoleName = "User";
                bool check = await _customerRepository.CreateCustomer(Customer);
                if (!check)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                await SendMailWithoutPassword(model.Email);

                var response = _mapper.Map<CustomerResponseModel>(Customer);
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register success. Please go to mail and verify account!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> RegisterCustomerByEmail(string googleId)
        {
            try
            {
                var payload = await GoogleJsonWebSignature.ValidateAsync(googleId);
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(payload.ExpirationTimeSeconds.Value).UtcDateTime;
                var currentTime = DateTime.UtcNow;
                if (currentTime > expirationTime)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Google id expired!."
                    };
                }

                string email = payload.Email;
                Customer checkExit = await _customerRepository.GetCustomerByEmail(email);

                if (checkExit != null)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 409,
                        Success = false,
                        Message = "Customer has been exit!.",
                    };
                }
                string password = GeneratePassword();
                string hashPassword = HashPassword(password);
                Customer customer = new Customer()
                {
                    Name = payload.Name,
                    Email = email,
                    Password = hashPassword,
                    CreatedAt = DateTime.UtcNow,
                    RoleName = "Customer",
                    IsDelete = false,
                    IsVerify = false,
                    Status = true,
                };
                bool check = await _customerRepository.CreateCustomer(customer);
                if (!check)
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"
                    };
                }
                await SendMailWithPassword(email, password);

                var response = _mapper.Map<CustomerResponseModel>(customer);
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 201,
                    Success = true,
                    Message = "Register success. Please go to mail and verify account!",
                    Data = response
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<BaseResponse> ResendVerificationEmail(string email)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmail(email);

                if (customer == null)
                {
                    return new BaseResponse
                    {
                        Code = 404,
                        Success = false,
                        Message = "Customer not found!"
                    };
                }

                if (customer.IsVerify)
                {
                    return new BaseResponse
                    {
                        Code = 400,
                        Success = false,
                        Message = "Account is already verified."
                    };
                }

                await SendMailWithoutPassword(customer.Email);

                return new BaseResponse
                {
                    Code = 200,
                    Success = true,
                    Message = "Verification email has been resent successfully."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 500,
                    Success = false,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse> SendMailWithoutPassword(string email)
        {
            try
            {
                Customer customer = await _customerRepository.GetCustomerByEmail(email);

                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ngocduongvua16@gmail.com", "ljdx zvbn zljh xopr");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ngocduongvua16@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "VERIFY YOUR ACCOUNT";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
    }
    .container {
      padding: 20px;
      background-color: #f4f4f4;
      border: 1px solid #ddd;
      border-radius: 5px;
      max-width: 600px;
      margin: 0 auto;
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 20px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Welcome to our Exchange Web!</div>
    <div class='content'>
      <p>Please click on the link to verify your account.</p>
<a href=""https://fe-parfum-web.vercel.app//verifyemail/" + customer.CustomerId + @""">click here</a>
    </div>
    <div class='footer'>
      &copy; 2024 Sport Shop. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse> SendMailWithPassword(string email, string password)
        {
            try
            {
                Customer customer = await _customerRepository.GetCustomerByEmail(email);
                var smtpClient = new SmtpClient("smtp.gmail.com");
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("ngocduongvua16@gmail.com", "ljdx zvbn zljh xopr");

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("ngocduongvua16@gmail.com");
                mailMessage.To.Add(email);
                mailMessage.Subject = "VERIFY YOUR ACCOUNT";

                mailMessage.Body = @"
<html>
<head>
  <style>
    body {
      font-family: Arial, sans-serif;
      line-height: 1.6;
    }
    .container {
      padding: 20px;
      background-color: #f4f4f4;
      border: 1px solid #ddd;
      border-radius: 5px;
      max-width: 600px;
      margin: 0 auto;
    }
    .header {
      font-size: 20px;
      font-weight: bold;
      text-align: center;
      margin-bottom: 20px;
    }
    .content {
      font-size: 16px;
      color: #333;
    }
    .footer {
      font-size: 12px;
      color: #888;
      text-align: center;
      margin-top: 20px;
    }
    .highlight {
      color: #007BFF;
      font-weight: bold;
    }
  </style>
</head>
<body>
  <div class='container'>
    <div class='header'>Welcome to our Sport Shop Web!</div>
    <div class='content'>
      <p>Please click on the link to verify your account.</p>
<a href=""https://fe-parfum-web.vercel.app//verifyemail/" + customer.CustomerId + @""">click here</a>
      <p>This is the login account and password if you need to login with userId and password.</p>
      <p>Email: <span class='highlight'>" + email + @"</span></p>
      <p>Password: <span class='highlight'>" + password + @"</span></p>
    </div>
    <div class='footer'>
      &copy; 2024 Sport Shop. All rights reserved.
    </div>
  </div>
</body>
</html>";

                mailMessage.IsBodyHtml = true;

                await smtpClient.SendMailAsync(mailMessage);

                return new BaseResponse
                {
                    Code = 200,
                    Message = "Send succeed."
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 400,
                    Message = "An error occurred: " + ex.Message
                };
            }
        }

        public async Task<BaseResponse<CustomerResponseModel>> UpdateCustomer(int id, UpdateRequestModel model)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerById(id);
                if (customer != null)
                {
                    var result = _mapper.Map(model, customer);
                    result.ModifiedDate = DateTime.Now;
                    await _customerRepository.UpdateCustomer(result);
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Update success!.",
                        Data = _mapper.Map<CustomerResponseModel>(result)
                    };
                }
                else
                {
                    return new BaseResponse<CustomerResponseModel>()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not found User!.",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<CustomerResponseModel>()
                {
                    Code = 500,
                    Success = false,
                    Message = "Server Error!"

                };
            }
        }

        public async Task<BaseResponse> VerifyAcccount(int id)
        {
            try
            {
                Customer customer = await _customerRepository.GetCustomerById(id);
                customer.IsVerify = true;
                customer.ModifiedDate = DateTime.Now;
                if (customer == null)
                {
                    return new BaseResponse()
                    {
                        Code = 404,
                        Success = false,
                        Message = "Not Found User!"
                    };
                }
                bool check = await _customerRepository.UpdateCustomer(customer);
                if (check)
                {
                    return new BaseResponse()
                    {
                        Code = 200,
                        Success = true,
                        Message = "Verify user success!"
                    };
                }
                else
                {
                    return new BaseResponse()
                    {
                        Code = 500,
                        Success = false,
                        Message = "Server Error!"

                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            try
            {
                byte[] hashBytes = Convert.FromBase64String(hashedPassword);
                byte[] salt = new byte[16];
                Array.Copy(hashBytes, 0, salt, 0, 16);
                byte[] hash = new byte[20];
                Array.Copy(hashBytes, 16, hash, 0, 20);

                var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
                byte[] computedHash = pbkdf2.GetBytes(20);

                for (int i = 0; i < 20; i++)
                {
                    if (hash[i] != computedHash[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string GenerateJwtToken(string customername, string roleName, int customerId)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim(ClaimTypes.Name, customername),
                    new Claim(ClaimTypes.Role, roleName),
                    new Claim(ClaimTypes.NameIdentifier, customerId.ToString())
                }),
                    Expires = DateTime.UtcNow.AddHours(24),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
