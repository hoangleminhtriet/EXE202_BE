using System;

namespace EunDeParfum_Service.RequestModel.Category
{
    public class GetAllCategoryRequestModel
    {
        public string? SearchKeyword { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
