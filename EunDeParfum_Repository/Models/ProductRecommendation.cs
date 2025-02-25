using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Repository.Models
{
    public class ProductRecommendation
    {
        [Key]
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal ScoreThreshold { get; set; }
        public bool Status { get; set; }

        public Product Product { get; set; }
    }

}
