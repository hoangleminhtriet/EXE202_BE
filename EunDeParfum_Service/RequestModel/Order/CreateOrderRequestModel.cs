using EunDeParfum_Service.RequestModel.Product;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Order
{
    public class CreateOrderRequestModel
    {
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }
        public List<ProductForOrderRequestModel> Products { get; set; }

    }
}
