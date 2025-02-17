using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Customer
{
    public class LoginResponseModel
    {
        public string token { get; set; }
        public CustomerResponseModel customer { get; set; }
    }
}
