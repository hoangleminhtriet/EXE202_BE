using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Order
{
    public class UpdateCartRequestModel
    {
        public int CustomerId { get; set; }
        public List<UpdateCartItem> Items { get; set; }
    }

    public class UpdateCartItem
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; } // Số lượng mới, nếu = 0 thì xóa sản phẩm
        public decimal Price { get; set; } // Giá mới (nếu cần cập nhật giá)
    }
}
