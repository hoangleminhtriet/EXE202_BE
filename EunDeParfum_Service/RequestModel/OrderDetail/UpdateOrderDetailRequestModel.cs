using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.OrderDetail
{
    public class UpdateOrderDetailRequestModel
    {
        [Required]
        public int OrderDetailId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
