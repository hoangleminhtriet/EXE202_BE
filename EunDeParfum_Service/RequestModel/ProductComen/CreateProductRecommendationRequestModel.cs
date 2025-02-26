using System;
using System.ComponentModel.DataAnnotations;

namespace EunDeParfum_Service.RequestModel.ProductRecommendation
{
    public class CreateProductRecommendationRequestModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "ScoreThreshold must be a positive value.")]
        public decimal ScoreThreshold { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
