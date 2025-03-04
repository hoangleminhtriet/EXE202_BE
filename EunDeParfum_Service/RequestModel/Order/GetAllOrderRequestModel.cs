using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.Order
{
    public class GetAllOrderRequestModel
    {
        public int pageNum { get; set; } = 1; // Số trang, mặc định là trang 1
        public int pageSize { get; set; } = 10; // Số lượng danh mục trên mỗi trang, mặc định là 10
    }
}
