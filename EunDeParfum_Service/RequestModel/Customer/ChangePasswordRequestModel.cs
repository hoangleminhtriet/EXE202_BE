using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Customer
{
    public class ChangePasswordRequestModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
