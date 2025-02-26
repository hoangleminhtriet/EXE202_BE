using System.Collections.Generic;

namespace EunDeParfum_Service.ResponseModel.ProductCategory
{
    public class ListProductCategoryResponseModel
    {
        public List<ProductCategoryResponseModel> Items { get; set; }
        public int TotalCount { get; set; } // Tổng số bản ghi
    }
}
