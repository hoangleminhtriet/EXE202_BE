using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.ProductRecoment
{
    public class ProductRecommendationResponseModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ScoreThreshold { get; set; }
    }
}
