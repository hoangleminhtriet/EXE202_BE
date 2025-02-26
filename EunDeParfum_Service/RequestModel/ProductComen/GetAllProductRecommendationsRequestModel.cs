using System;

namespace EunDeParfum_Service.RequestModel.ProductRecommendation
{
    public class GetAllProductRecommendationsRequestModel
    {
        public int? ProductId { get; set; } // Optional, if you want to filter by ProductId
        public bool? Status { get; set; }   // Optional, if you want to filter by status

        // Pagination
        public int PageNum { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
