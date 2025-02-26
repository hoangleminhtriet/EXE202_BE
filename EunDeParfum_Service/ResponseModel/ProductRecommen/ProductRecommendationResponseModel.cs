namespace EunDeParfum_Service.ResponseModel.ProductRecommendation
{
    public class ProductRecommendationResponseModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ScoreThreshold { get; set; }
        public bool Status { get; set; }

        public string ProductName { get; set; } // Optional: If you want to include product name in the response
    }
}
