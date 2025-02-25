namespace EunDeParfum_Service.RequestModel.ProductRecommendation
{
    public class UpdateProductRecommendationRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}
