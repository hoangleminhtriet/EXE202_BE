using EunDeParfum_Service.RequestModel.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.OrderDetail
{
    public class CreateOrderDetailRequestModel
    {
        public int OrderId { get; set; }
        public List<ProductForOrderRequestModel> Products { get; set; }
    }
}
