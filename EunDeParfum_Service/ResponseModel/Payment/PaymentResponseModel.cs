using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EunDeParfum_Service.ResponseModel.Payment
{
    public class PaymentResponseModel
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public bool isDeleted { get; set; }
        public string? TransactionId { get; set; }
        public string PaymentMethod { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CheckoutUrl { get; set; }
    }
}
