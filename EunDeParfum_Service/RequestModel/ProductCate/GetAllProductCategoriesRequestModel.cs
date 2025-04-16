using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.ProductCate
{
    public class GetAllProductCategoriesRequestModel
    {
        public int pageNum { get; set; } = 1;
        public int pageSize { get; set; } = 1;
        public string? keyWord { get; set; }
        public bool? Status { get; set; }
    }
}
