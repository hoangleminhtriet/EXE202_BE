using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Customer
{
    public class GetAllCustomerRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 1;
        public string? keyWord { get; set; }
        public string? role { get; set; }
        public bool? status { get; set; }
        public bool? is_Verify { get; set; }
        public bool? is_Delete { get; set; }
    }
}
