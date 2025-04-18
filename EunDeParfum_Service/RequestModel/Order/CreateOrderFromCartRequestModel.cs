using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Order
{
    public class CreateOrderFromCartRequestModel
    {
        public int CustomerId { get; set; }
        public List<int> OrderDetailIds { get; set; } // Danh sách OrderDetailId được chọn
    }
}
