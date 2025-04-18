using EunDeParfum_Service.ResponseModel.OrderDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Order
{
    public class OrderReponseModel
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public bool IsDeleted { get; set; }
        public string CheckoutUrl { get; set; }
        public List<OrderDetailResponseModel> OrderDetails { get; set; }

    }
}
