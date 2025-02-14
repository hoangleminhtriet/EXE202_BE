﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Customer
{
    public class CustomerResponseModel
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string RoleName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool Status { get; set; }
        public bool IsDelete { get; set; }
        public bool IsVerify { get; set; }
    }
}
