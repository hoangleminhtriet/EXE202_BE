using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Order
{
    public class AddToCartRequestModel
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<CartProduct> Products { get; set; } = new List<CartProduct>();
    }

    public class CartProduct
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
