﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Customer
{
    public class RegisterRequestModel
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
