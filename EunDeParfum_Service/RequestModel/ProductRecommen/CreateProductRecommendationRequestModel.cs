using System;
using System.ComponentModel.DataAnnotations;

namespace EunDeParfum_Service.RequestModel.ProductRecommendation
{
    public class CreateProductRecommendationRequestModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "ScoreThreshold must be between 0 and 1.")]
        public decimal ScoreThreshold { get; set; }
    }
}
