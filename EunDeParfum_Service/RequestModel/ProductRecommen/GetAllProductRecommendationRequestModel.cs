using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.RequestModel.ProductRecommendation
{
    public class GetAllProductRecommendationRequestModel
    {
        public int? ProductId { get; set; }
        public decimal? MinScoreThreshold { get; set; }
        public decimal? MaxScoreThreshold { get; set; }
    }
}
