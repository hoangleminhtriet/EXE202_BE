namespace EunDeParfum_Service.RequestModel.ProductCategory
{
    public class CreateProductCategoryRequestModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public bool Status { get; set; }
    }
}
