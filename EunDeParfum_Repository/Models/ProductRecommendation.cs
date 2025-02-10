using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Models
{
    public class ProductRecommendation
    {
        public int RecommendationId { get; set; }
        public int ProductId { get; set; }
        public decimal ScoreThreshold { get; set; }

        public Product Product { get; set; }
    }

}
