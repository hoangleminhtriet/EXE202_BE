using EunDeParfum_Repository.Models;
using EunDeParfum_Service.RequestModel.Customer;
using EunDeParfum_Service.ResponseModel.BaseResponse;
using EunDeParfum_Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Linq;

namespace EXE201_EunDeParfum.Controllers
{
    [Route("API/Customer/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService services)
        {
            _service = services;
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> RegisterAdmin(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountAdmin(email, password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Staff")]
        public async Task<IActionResult> RegisterStaff(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountStaff(email, password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("Manager")]
        public async Task<IActionResult> RegisterManager(string email, string password, string name)
        {
            try
            {
                var result = await _service.CreateAccountManager(email, password, name);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Email")]
        public async Task<IActionResult> RegisterEmail(string googleId)
        {
            try
            {
                var result = await _service.RegisterCustomerByEmail(googleId);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            try
            {
                var result = await _service.RegisterCustomer(model);
                return StatusCode(result.Code, result);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("Verify/{id}")]
        public async Task<IActionResult> VerifyAccount(int id)
        {
            try
            {
                var result = await _service.VerifyAcccount(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("block/{id}")]
        public async Task<IActionResult> BlockCustomer(int id)
        {
            try
            {
                var result = await _service.BlockCustomer(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestModel model)
        {
            try
            {
                var result = await _service.Login(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("LoginMail")]
        public async Task<IActionResult> LoginMail(string googleId)
        {
            try
            {
                var result = await _service.LoginMail(googleId);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Search")]
        public async Task<IActionResult> GetAllCustomer(GetAllCustomerRequestModel model)
        {
            try
            {
                var result = await _service.GetListCustomer(model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            try
            {
                var result = await _service.GetCustomerById(id);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, UpdateRequestModel model)
        {
            try
            {
                var result = await _service.UpdateCustomer(id, model);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost("Change-Status/{id}")]
        public async Task<IActionResult> DeleteCustomer(int id, bool status)
        {
            try
            {
                var result = await _service.DeleteCustomer(id, status);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("Current-Customer")]
        public async Task<IActionResult> GetCurrentCustomer()
        {
            try
            {
                var customerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (customerId == null)
                {
                    return StatusCode(401, new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Customer information not found, customer may not be authenticated into the system!."
                    });
                }
                else
                {
                    var result = await _service.GetCustomerById(int.Parse(customerId));
                    return StatusCode(result.Code, result);
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [Authorize]
        [HttpPost("Change-Password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel model)
        {
            try
            {
                // Get customerId form JWT
                var customerIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


                if (customerIdClaim == null)
                {
                    return StatusCode(401, new BaseResponse()
                    {
                        Code = 401,
                        Success = false,
                        Message = "Customer information not found, customer may not be authenticated into the system!"
                    });
                }

                int customerId = int.Parse(customerIdClaim);

                //Call ChangePassword service
                var result = await _service.ChangePassword(customerId, model.CurrentPassword, model.NewPassword);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpPost("Resend-Verification")]
        public async Task<IActionResult> ResendVerificationEmail([FromBody] string email)
        {
            try
            {
                var result = await _service.ResendVerificationEmail(email);
                return StatusCode(result.Code, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }



    }
}
