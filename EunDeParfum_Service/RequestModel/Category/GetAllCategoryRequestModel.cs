using System;

namespace EunDeParfum_Service.RequestModel
{
    public class GetAllCategoryRequestModel
    {
        public int pageNum { get; set; } = 1; // Số trang, mặc định là trang 1
        public int pageSize { get; set; } = 10; // Số lượng danh mục trên mỗi trang, mặc định là 10
        public string? keyWord { get; set; } // Từ khóa tìm kiếm, có thể là tên danh mục hoặc mô tả
        public bool? Status { get; set; } // Trạng thái của danh mục, có thể là true (hoạt động) hoặc false (khóa)
    }
}
