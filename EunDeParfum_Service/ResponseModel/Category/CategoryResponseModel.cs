namespace EunDeParfum_Service.ResponseModel
{
    public class CategoryResponseModel
    {
        public int CategoryId { get; set; } // ID của danh mục
        public string Name { get; set; } // Tên danh mục
        public string Description { get; set; } // Mô tả danh mục
        public bool Status { get; set; } // Trạng thái của danh mục (true: hoạt động, false: khóa)
    }
}
