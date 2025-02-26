namespace EunDeParfum_Service.RequestModel.ProductCategory
{
    public class UpdateProductCategoryRequestModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; } // true: hoạt động, false: không hoạt động
    }
}
